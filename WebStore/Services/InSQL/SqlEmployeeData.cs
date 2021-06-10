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
    public class SqlEmployeeData 
    {
        private readonly WebStoreDB _db;

        public SqlEmployeeData(WebStoreDB db) => _db = db;

    }
}
