using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB _db;

        //public SqlEmployeeData(WebStoreDB db) => _db = db;

        private int _CurrentMaxId;

        public SqlEmployeeData(WebStoreDB db)
        {
            _db = db;
            _CurrentMaxId = _db.Employees.Max(i => i.Id);
        }

        public int Add(Domain.Entities.Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_db.Employees.Contains(employee)) return employee.Id;

            employee.Id = ++_CurrentMaxId;
            _db.Employees.Add(employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();

            //var db_item = Get(id);
            //if (db_item is null) return false;
            //return _db.Employees.Remove(db_item);
        }

        public Domain.Entities.Employee Get(int id) => _db.Employees.SingleOrDefault(employee => employee.Id == id);
        

        public IEnumerable<Domain.Entities.Employee> GetAll() => _db.Employees;
        

        public void Update(Domain.Entities.Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_db.Employees.Contains(employee)) return;

            var db_item = Get(employee.Id);
            if (db_item is null) return;


            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;
        }
    }
}
