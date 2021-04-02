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
        public IActionResult Index()
        {
            var listOfProducts = _ss.GetAllProducts();
            return View(listOfProducts);
        }
    }
}
