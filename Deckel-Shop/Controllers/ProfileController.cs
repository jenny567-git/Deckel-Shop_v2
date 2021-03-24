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
            return View("views/profile/Customer/index.cshtml");
        }

        public IActionResult Customer()
        {
            return View();
        }
        
        public IActionResult Administrator()
        {
            return View();
        }
    }
}
