using Deckel_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProfileController : Controller
    {
            
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View("views/profile/administrator/index.cshtml");
            }
            Database.Models.DeckelShopContext ctx = new Database.Models.DeckelShopContext();


            return View("views/profile/Customer/index.cshtml", ctx.Customers);
        }
        public IActionResult CustomerOrderHistory()
        {
            return View("views/profile/Customer/OrderHistory.cshtml");
        }

        public IActionResult Customer()
        {
            return View("views/profile/Customer/index.cshtml");
        }

        public IActionResult Administrator()
        {
            var customer = new List<Customer>()
            {
                 new Customer() { Id = 1, Date = DateTime.Now, Name = "John", Amount = 2, TotalPrice = 222 } ,
                  new Customer() { Id = 2, Date = DateTime.Now, Name = "Fisko", Amount = 6, TotalPrice = 1000 } ,
            };
            return View("/views/profile/administrator/index.cshtml", customer);
        }

        public IActionResult Admin_customerList()
        {
            return View("/views/profile/administrator/Admin_customerList.cshtml");
        }

        public IActionResult Admin_customerOrderHistory()
        {
            return View("/views/profile/administrator/Admin_customerOrderHistory.cshtml");
            return View("views/Profile/Administrator/index.cshtml");
        }

        public IActionResult Stock()
        {
            return View("views/Stock/index.cshtml");
        }

        public IActionResult DeliveredOrders()
        {
            var deliverdOrders = new List<Customer>()
            {
                 new Customer() { Id = 1, Date = DateTime.Now, Name = "John", Amount = 2, TotalPrice = 222 } ,
                  new Customer() { Id = 2, Date = DateTime.Now, Name = "Fisko", Amount = 6, TotalPrice = 1000 } ,
            };
            return View("views/profile/Administrator/DeliveredOrders.cshtml", deliverdOrders);
        }
    }
}
