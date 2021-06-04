using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryEmployesData : IEmployeesData
    {
        #region Новый объект
        private readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 31 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
        };
        #endregion

        #region старые данные 
        //private readonly List<Employee> _Employees = new()
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


        private int _CurrentMaxId;

        public InMemoryEmployesData()
        {
            _CurrentMaxId = _Employees.Max(i => i.Id);
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public Employee Get(int id) => _Employees.SingleOrDefault(employee => employee.Id == id);

        //public object GetOld(int id) => _Employees.SingleOrDefault(employee => employee.Id == id).detailsData;
        
        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return employee.Id; 

            employee.Id = ++_CurrentMaxId;
            _Employees.Add(employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if(_Employees.Contains(employee)) return; 

            var db_item = Get(employee.Id);
            if(db_item is null) return;

            
            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;

        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null) return false;
            return _Employees.Remove(db_item);
        }
    }
}
