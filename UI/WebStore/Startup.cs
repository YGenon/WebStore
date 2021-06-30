using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Interfaces.Services;
using WebStore.Data;
using System;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Clients.Values;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Orders;
using WebStore.Services.Services.InCookies;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MSSQL")));
            services.AddTransient<WebStoreDBInitializer>();

#region подключаем Identity

            services.AddIdentity<User, Role>(/*opt => { }*/)
               .AddEntityFrameworkStores<WebStoreDB>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif

                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });
#endregion

            //регистрируем наш сервис

            //services.AddSingleton<IEmployeesData, InMemoryEmployesData>();
            //services.AddScoped<IEmployeesData, SqlEmployeeData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            //services.AddScoped<IProductData, SqlProductData>();
            //services.AddScoped<IOrderService, SqlOrderService>();

            //services.AddHttpClient<IProductData, ProductsClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            //services.AddHttpClient<IOrderService, OrdersClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            //services.AddHttpClient<IEmployeesData, EmployeesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            //services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));

            services.AddHttpClient("WebStoreAPI", client => client.BaseAddress = new Uri(Configuration["WebAPI"]))
               .AddTypedClient<IValuesService, ValuesClient>()
               .AddTypedClient<IEmployeesData, EmployeesClient>()
               .AddTypedClient<IProductData, ProductsClient>()
               .AddTypedClient<IOrderService, OrdersClient>()
                ;

            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            using (var scope = services.CreateScope()) 
                scope.ServiceProvider.GetRequiredService<WebStoreDBInitializer>().Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            //var greetings = Configuration["Greetings"];
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    //await context.Response.WriteAsync(greetings);
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });
                               
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
