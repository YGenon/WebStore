using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }


        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Order>()
               .HasMany(order => order.Items)
               .WithOne(item => item.Order)
               .OnDelete(DeleteBehavior.Cascade);

            model.Entity<User>()
               .HasMany<Order>()
               .WithOne(order => order.User)
               .OnDelete(DeleteBehavior.Cascade);

            model.Entity<OrderItem>()
               .HasOne(item => item.Product)
               .WithMany()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
