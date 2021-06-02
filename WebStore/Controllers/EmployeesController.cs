﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
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
        public IActionResult Index()
        {
            return View(__Employees);
        }

        public IActionResult Details(int id)
        {
            ViewData["IdEmploees"] = id;
            return View(__Employees);


        }
    }
}