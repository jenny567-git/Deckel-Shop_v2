using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Cart cart)
        {
            if (cart == null)
            {
                cart = new Cart
                {
                    Products = new List<Product>()
                };
            }

            return View("Default", cart);
        }

        private Cart GetShoppingCartContents()
        {
            var shoppingcart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

            return shoppingcart;
        }
    }
}
