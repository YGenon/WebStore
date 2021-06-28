using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _ValuesService;

        public WebAPIController(IValuesService ValuesService) => _ValuesService = ValuesService;

        public IActionResult Index()
        {
            var values = _ValuesService.GetAll();
            return View(values);
        }
    }
}
