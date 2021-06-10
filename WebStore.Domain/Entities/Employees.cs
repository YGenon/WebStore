using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;


namespace WebStore.Domain.Entities
{
    public class Employees : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }

        //public List<Details> detailsData { get; set; }
    }
}
