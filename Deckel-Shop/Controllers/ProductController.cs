using Deckel_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<Cap> caplist = new List<Cap>()
            {
            new Cap() { Id = 1, ProductName = "pName", Price = 12, Stock = 20 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 2, ProductName = "pName2", Price = 2, Stock = 30 },
            new Cap() { Id = 3, ProductName = "pName3", Price = 22, Stock = 40 }

            };
            return View(caplist);
        }
    }
}
