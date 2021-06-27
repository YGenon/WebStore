using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products.OrderBy(p => p.Order).ToView()
                //Products = products
                //   .OrderBy(p => p.Order)
                //   .Select(p => new ProductViewModel
                //    {
                //        Id = p.Id,
                //        Name = p.Name,
                //        Price = p.Price,
                //        ImageUrl = p.ImageUrl,
                //    })
            });
        }

        public IActionResult Details(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            if (product is null)
                return NotFound();
           
            return View(product.ToView());
        }
    }
}
