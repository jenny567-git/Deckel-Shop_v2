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
            
            return View("views/profile/Customer/OrderHistory.cshtml");
        }

        //--------------------------------------------------------------------START OF ORDERS
        
        //PEDNING ORDERS
        public IActionResult Administrator()
        {

            //OrderService os = new OrderService();


            return View("/views/profile/administrator/index.cshtml", _os.GetAllOrdersByOrderStatus("Not Delivered"));
        }

        public IActionResult DeliveredOrders()
        {
            return View("views/profile/Administrator/DeliveredOrders.cshtml", _os.GetAllOrdersByOrderStatus("Delivered"));
        }

        [HttpPost]

        public async Task< IActionResult> SendOrder(int id)
        {
            await _os.SendOrder(id);
            return View("/views/profile/administrator/DeliveredOrders.cshtml", _os.GetAllOrdersByOrderStatus("Delivered"));
        }

        //--------------------------------------------------------------------END OF ORDERS

        //--------------------------------------------------------------------START OF CUSTOMER
        public IActionResult Admin_customerList()
        {
            //var listOfCustomers = _cs.GetAllCustomers();
            return View("/views/profile/administrator/Admin_customerList.cshtml", _cs.GetAllCustomers());
        }


        public IActionResult CustomerOrderHistory(int id)
        {
            return View("views/profile/Customer/OrderHistory.cshtml", _os.GetAllOrdersBySelectedCustomer(id));
        }

        public IActionResult Customer(int id)
        {
            return View("views/profile/Customer/OrderHistory.cshtml", _os.GetAllOrdersBySelectedCustomer(id));
        }

        [HttpPost]
        public IActionResult AddCustomer([FromForm] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _cs.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> EditCustomer([FromForm] Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                await _cs.EditCustomer(customer);
                
            }
            return RedirectToAction(nameof(Admin_customerList));
        }

        [HttpPost]

        public IActionResult RemoveCustomer(int id)
        {
            _cs.RemoveCustomer(id);
            return View("/views/profile/administrator/Admin_customerList.cshtml", _cs.GetAllCustomers());
        }

        public IActionResult Admin_customerOrderHistory(int id)
        {
            
            return View("/views/profile/administrator/Admin_customerOrderHistory.cshtml", _os.GetAllOrdersBySelectedCustomer(id));

        }
        //--------------------------------------------------------------------END OF CUSTOMER

        //--------------------------------------------------------------------START OF PRODUCTS
        public IActionResult Stock(int filter)
        {
            switch (filter)
            {
                case 1:
                    return View("views/profile/administrator/Stock.cshtml", _ss.GetAllAvailableProducts());
                case 2:
                    return View("views/profile/administrator/Stock.cshtml", _ss.GetAllRemovedProducts());
                default:
                    return View("views/profile/administrator/Stock.cshtml", _ss.GetAllProducts());
                 
            }
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                _ss.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]

        public IActionResult AddProductBackToStock(int id, int StockAmount)
        {
            _ss.AddBackToStock(id,StockAmount);
            return View("views/profile/administrator/Stock.cshtml", _ss.GetAllProducts());
        }
    

        [HttpPost]

        public IActionResult RemoveProduct(int id)
        {
            _ss.RemoveProduct(id);
            return View("views/profile/administrator/Stock.cshtml",_ss.GetAllProducts());
        }

        //--------------------------------------------------------------------END OF PRODUCTS
    }
}
