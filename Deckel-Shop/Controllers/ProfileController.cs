using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }
        
        public IActionResult Administrator()
        {
            return View("/views/profile/administrator/index.cshtml");
        }

        public IActionResult Admin_customerList()
        {
            return View("/views/profile/administrator/Admin_customerList.cshtml");
        }
    }
}
