using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;
using WebStore.Domain.Entities;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        #region старые данные 
        //private static readonly List<Employee> __Employees = new()
        //{
        //    new Employee
        //    {
        //        Id = 1,
        //        LastName = "Иванов",
        //        FirstName = "Иван",
        //        Patronymic = "Иванович",
        //        Age = 27,
        //        detailsData = new List<Details>()
        //                        {
        //                            new Details {mail = "ivanov@mail.com", phone = "111-111-1111"},
        //                            new Details {mail = "ivanovIvan@mail.com", phone = "111-122-2121"}
        //                         }
        //    },
        //    new Employee
        //    {
        //        Id = 2,
        //        LastName = "Петров",
        //        FirstName = "Пётр",
        //        Patronymic = "Петрович",
        //        Age = 31,
        //        detailsData = new List<Details>()
        //                        {
        //                            new Details {mail = "petrov@mail.com", phone = "222-222-2222"}
        //                         }
        //    },
        //    new Employee
        //    {
        //        Id = 3,
        //        LastName = "Сидоров",
        //        FirstName = "Сидор",
        //        Patronymic = "Сидорович",
        //        Age = 18,
        //        detailsData = new List<Details>()
        //                        {
        //                            new Details {mail = "sidorov@mail.com", phone = "333-333-3333"}
        //                         }
        //    },
        //};
        #endregion

        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }

        public IActionResult Details(int id)
        {
            #region мой старый код
            //ViewData["IdEmploees"] = id;
            //return View(_EmployeesData.Get(id));
            #endregion

            var employee = _EmployeesData.Get(id);
            
            if (employee == null) return NotFound();

            return View(employee);
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.Get((int)id);
            if (employee is null)
                return NotFound();

            var view_model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = (int)employee.Age,
            };
            return View(view_model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
            };

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = (int)employee.Age,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
