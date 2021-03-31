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
            if (User.IsInRole("Administator"))
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
            return View("/views/profile/administrator/index.cshtml");
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
            return View("views/profile/Administrator/DeliveredOrders.cshtml");
        }
    }
}
