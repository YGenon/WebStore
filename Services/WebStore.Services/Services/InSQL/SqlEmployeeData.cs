using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB _db;       

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
            _db.SaveChanges();
            return employee.Id;
        }

        public bool Delete(int id)
        {
            //throw new NotImplementedException();

            var employee = Get(id);
            if (employee is null) return false;

            #region код нужно поправить
            // TODO: выдает ошибку - не может преобразовать в bool
             _db.Employees.Remove(employee);

            //TODO: заглушка
            //return true;
            #endregion

            _db.SaveChanges();
            return true;
        }

        public Domain.Entities.Employee Get(int id) => _db.Employees.SingleOrDefault(employee => employee.Id == id);
        

        public IEnumerable<Domain.Entities.Employee> GetAll() => _db.Employees;
        

        public void Update(Domain.Entities.Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            _db.Update(employee);
            _db.SaveChanges();
            //if (_db.Employees.Contains(employee)) return;

            //var db_item = Get(employee.Id);
            //if (db_item is null) return;


            //db_item.LastName = employee.LastName;
            //db_item.FirstName = employee.FirstName;
            //db_item.Patronymic = employee.Patronymic;
            //db_item.Age = employee.Age;
        }
    }
}
