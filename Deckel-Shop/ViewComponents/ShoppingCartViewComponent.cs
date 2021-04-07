using Database.Models;
using Deckel_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        public ShoppingCartViewComponent()
        {

        }

        public IViewComponentResult Invoke(Cart cart)
        {
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = 0,
                    CustomerName = "N/A",
                    CustomerId = 0,
                    Products = new List<Product>()
                };
            }

            return View("Default", cart);
        }

        private Cart GetShoppingCartContents(int customerId)
        {
            var shoppingcart = new Cart
            {
                CustomerId = customerId,
                CustomerName = "John Diligence",
                TotalPrice = 87.34,
                Products = new List<Product> {
                    new Product {Id = 1, Name = "Paddleboard", Price = 50.00M, Description = "Big board for paddeling.", Amount = 15},
                    new Product {Id = 2, Name = "Paddle", Price = 10.00M, Description = "Composite boar to paddle.", Amount = 18},
                    new Product {Id = 3, Name = "Lifevest", Price = 27.34M, Description = "Life vest for floating in the water.", Amount = 21}
                }
            };

            return shoppingcart;
        }
    }
}
