using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Infrastructure.Mapping;
using WebStore.Models;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    //[Controller]
    public class HomeController : Controller
    {     


        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) { _Configuration = Configuration; }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index([FromServices] IProductData ProductData)
        {
            ViewBag.Products = ProductData.GetProducts().Take(9).ToView();
            return View();
        }

        //public IActionResult Index([FromServices] IProductData ProductData)
        //{
        //    var products = ProductData
        //       .GetProducts()
        //       .Take(9)
        //       .Select(p => new ProductViewModel
        //       {
        //           Id = p.Id,
        //           Name = p.Name,
        //           Price = p.Price,
        //           ImageUrl = p.ImageUrl,
        //       });

        //    ViewBag.Products = products;
        //    //ViewData["Products"] = products;

        //    return View();
        //}

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Greetings"]);
            //return View("Index");
        }

        //public IActionResult Employees()
        //{
        //    return View(__Employees);
        //}        

        public IActionResult Shop() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Checkout() => View();
        public IActionResult Cart() => View();
        public IActionResult Login() => View();

        public IActionResult Blogs() => View();
        public IActionResult BlogSingle() => View();

        public IActionResult Error_404() => View();

        public IActionResult Contact() => View();
    }
}
