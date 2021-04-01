using Database.Models;
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
        private CustomerService _customerService { get; set; }
        public ProfileController()
        {
            _customerService = new CustomerService();
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

        public IActionResult Administrator()
        {
            var customer = new List<Models.Customer>()
            {
                 new Models.Customer() { Id = 1, Date = DateTime.Now, Name = "John", Amount = 2, TotalPrice = 222 } ,
                  new Models.Customer() { Id = 2, Date = DateTime.Now, Name = "Fisko", Amount = 6, TotalPrice = 1000 } ,
            };
            return View("/views/profile/administrator/index.cshtml", customer);
        }

        [HttpGet]
        public IActionResult Admin_customerList(string option, int id, string search)
        {
           

            var result = _customerService.GetAllCustomers();

            if (option == "Id")
            {
                int stringToId = int.Parse(search);

                var result1 = _customerService.GetCustomerById(stringToId);
                return View("views/Profile/Administrator/Admin_customerList.cshtml", result1);
            }
            else if (option == "FirstName")
            {
                var result2 = _customerService.GetGustomerByFirstName(search);
                return View("views/Profile/Administrator/Admin_customerList.cshtml", result2);
            }
            else if (option == "LastName")
            {
                var result3 = _customerService.GetCustomerByLastName(search);
                return View("views/Profile/Administrator/Admin_customerList.cshtml", result3);
            }


            return View("views/Profile/Administrator/Admin_customerList.cshtml", result);
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
            var deliverdOrders = new List<Models.Customer>()
            {
                 new Models.Customer() { Id = 1, Date = DateTime.Now, Name = "John", Amount = 2, TotalPrice = 222 } ,
                  new Models.Customer() { Id = 2, Date = DateTime.Now, Name = "Fisko", Amount = 6, TotalPrice = 1000 } ,
            };
            return View("views/profile/Administrator/DeliveredOrders.cshtml", deliverdOrders);
        }
    }
}
