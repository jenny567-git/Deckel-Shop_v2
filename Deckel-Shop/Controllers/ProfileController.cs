using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly CustomerService _cs;
        private readonly StockService _ss;
        private readonly OrderService _os;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> um)
        {
            _cs = new CustomerService();
            _ss = new StockService();
            _os = new OrderService();
            _userManager = um;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View("views/profile/administrator/index.cshtml", _os.GetAllOrdersByOrderStatus("Pending"));
            }

            return View("views/profile/Customer/OrderHistory.cshtml", _os.GetAllOrdersBySelectedCustomer(_cs.GetCustomerId(User.Identity.Name)));
        }

        public IActionResult Users()
        {

            return View("views/profile/Administrator/Users.cshtml");
        }

        public async Task<IActionResult> ChangeUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (await _userManager.IsInRoleAsync(user, "Customer"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            else if (await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrator");
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                Debug.WriteLine("Error: Could not associate a user with a role");
            }

            return View("views/profile/Administrator/Users.cshtml");
        }


        //--------------------------------------------------------------------START OF ORDERS

        //PEDNING ORDERS
        public IActionResult Administrator()
        {

            //OrderService os = new OrderService();


            return View("/views/profile/administrator/index.cshtml", _os.GetAllOrdersByOrderStatus("Pending"));
        }

        public IActionResult DeliveredOrders(string? filter)
        {
            var list = _os.GetAllOrdersByOrderStatusNotPending();
            switch (filter)
            {
                case "delivered":
                    list = _os.GetAllOrdersByOrderStatus("Delivered");
                    break;
                case "cancelled":
                    list = _os.GetAllOrdersByOrderStatus("Cancelled");
                    break;
                default:
                    break;
            }
            return View("views/profile/Administrator/DeliveredOrders.cshtml", list);
        }

        [HttpPost]

        public async Task< IActionResult> SendOrder(int id)
        {
            await _os.SendOrder(id);
            return RedirectToAction(nameof(DeliveredOrders));
        }
        
        [HttpPost]

        public async Task< IActionResult> RemoveOrder(int id)
        {
            _ss.UpdateStockWhenCancelledOrder(id);
            await _os.RemoveOrder(id);
            //_ss.UpdateStock(id, _ss.GetStockStatusByProductId(id));
            return RedirectToAction(nameof(Administrator));
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
        public IActionResult Stock(string filter)
        {
            var sort = _ss.GetAllProducts();
            switch (filter)
            {
                case "avail_prod":
                    sort = _ss.GetAllAvailableProducts();
                    break;
                case "removed_prod":
                    sort = _ss.GetAllRemovedProducts();
                    break;
                case "name":
                    sort = sort.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    sort = sort.OrderByDescending(p => p.Name);
                    break;
                case "price_asc":
                    sort = sort.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    sort = sort.OrderByDescending(p => p.Price);
                    break;
                case "amount_desc":
                    sort = sort.OrderBy(p => p.Amount);
                    break;
                case "amount_asc":
                    sort = sort.OrderByDescending(p => p.Amount);
                    break;
                default:
                    sort = _ss.GetAllProducts();
                    break;

            }
            return View("views/profile/administrator/Stock.cshtml", sort);
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
            _ss.AddBackToStock(id, StockAmount);
            return View("views/profile/administrator/Stock.cshtml", _ss.GetAllProducts());
        }


        [HttpPost]

        public IActionResult RemoveProduct(int id)
        {
            _ss.RemoveProduct(id);
            return View("views/profile/administrator/Stock.cshtml", _ss.GetAllProducts());
        }

        //--------------------------------------------------------------------END OF PRODUCTS
    }
}
