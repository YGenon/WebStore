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

        public SqlEmployeeData(WebStoreDB db) => _db = db;

        public int Add(Domain.Entities.Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Entities.Employee> GetAll() => _db.Employees;
        

        public void Update(Domain.Entities.Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
