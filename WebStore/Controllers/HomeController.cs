using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Controller]
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee
            {
                Id = 1,
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 27,
                detailsData = new List<Details>()
                                {
                                    new Details {mail = "ivanov@mail.com", phone = "111-111-1111"},
                                    new Details {mail = "ivanovIvan@mail.com", phone = "111-122-2121"}
                                 }
            },
            new Employee
            {
                Id = 2,
                LastName = "Петров",
                FirstName = "Пётр",
                Patronymic = "Петрович",
                Age = 31,
                detailsData = new List<Details>()
                                {
                                    new Details {mail = "petrov@mail.com", phone = "222-222-2222"}
                                 }
            },
            new Employee
            {
                Id = 3,
                LastName = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 18,
                detailsData = new List<Details>()
                                {
                                    new Details {mail = "sidorov@mail.com", phone = "333-333-3333"}
                                 }
            },
        };


        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) { _Configuration = Configuration; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Greetings"]);
            //return View("Index");
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }

        public IActionResult Details(int id)
        {
            ViewData["IdEmploees"] = id;
            return View(__Employees);


        }

        public IActionResult Error_404() => View();
        public IActionResult Blogs() => View();
        public IActionResult BlogSingle() => View();
    }
}
