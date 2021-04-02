using Deckel_Shop.Models;
using Deckel_Shop.Services;
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
        private readonly CustomerService _cs;
        private readonly StockService _ss;
        private readonly OrderService _os;

        public ProfileController()
        {
            _cs = new CustomerService();
            _ss = new StockService();
            _os = new OrderService();

        }

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
        
        public IActionResult AddCustomer()
        {
            return View("views/profile/Administrator/AddCustomer.cshtml");
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
            var listOfCustomers = _cs.GetAllCustomers();
            return View("/views/profile/administrator/Admin_customerList.cshtml", listOfCustomers);
        }

        public IActionResult Admin_customerOrderHistory(int id)
        {
            return View("/views/profile/administrator/Admin_customerOrderHistory.cshtml", _os.GetAllOrdersByCustomerId(id));

        }

        public IActionResult Stock()
        {
            return View("views/Stock/index.cshtml", _ss.GetAllProducts());
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
