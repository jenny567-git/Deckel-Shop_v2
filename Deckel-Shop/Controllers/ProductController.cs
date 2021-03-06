using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly StockService _ss;

        public ProductController()
        {
            _ss = new StockService();
        }

        public IActionResult Index(string sortOrder)
        {

            var listOfProducts = _ss.GetAllAvailableProducts();
            switch (sortOrder)
            {
                case "name_desc":
                    listOfProducts = listOfProducts.OrderByDescending(p => p.Name);
                    break;
                case "price_desc":
                    listOfProducts = listOfProducts.OrderByDescending(p => p.Price);
                    break;
                case "price_asc":
                    listOfProducts = listOfProducts.OrderBy(p => p.Price);
                    break;
                default:
                    listOfProducts = listOfProducts.OrderBy(p => p.Name);
                    break;
            }
            return View(listOfProducts);
        }

    }
}
