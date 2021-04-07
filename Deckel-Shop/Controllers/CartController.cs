using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Deckel_Shop.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly StockService _ss = new StockService();

        private static List<Product> productlist = new List<Product>();

        //TEST OBJECTS: Customer, Products, Cart
        private static Customer customer = new Customer
        {
            Id = 1,
            FirstName = "Blas De Lezo"
        };

        private Cart cart = new Cart
        {
            Id = 27,
            CustomerId = customer.Id,
            CustomerName = customer.FirstName,
            Products = productlist
        };

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult kjhslkijgfhlisdfujhgidslfuhdilfuhdifubhdifugbh()
        //{

        //    var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

        //    if (shopCart != null)
        //    {
        //        products = shopCart.Products;
        //    }

        //    return View(products);
        //}


        public IActionResult AddProductToCart(int id)
        {
            var product = _ss.GetProduct(id);
            productlist = _ss.GetAllAvailableProducts().ToList();

            if (!ModelState.IsValid)
            {
                RedirectToAction("views/product/index.cshtml");
            }

            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

            if (shopCart == null)
            {
                shopCart = new Cart
                {
                    Id = cart.Id,
                    CustomerId = cart.CustomerId,
                    CustomerName = cart.CustomerName,
                    TotalPrice = cart.TotalPrice,
                    Products = new List<Product>()
                };
            }

            if (shopCart.Products.Exists(p => p.Id == product.Id))
            {
                ++shopCart.Products.First(p => p.Id == product.Id).Amount;
            }
            else
            {
                shopCart.Products.Add(
                    new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Amount = 1
                    }) ;
            }

            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);

            return View("SavedCart", shopCart);
        }


        //public IActionResult Session()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        RedirectToAction("Index");
        //    }

        //    Cart shoppingCart = null;

        //    shoppingCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

        //    return View(shoppingCart);
        //}


        //public IActionResult AddVarsToSession(int age, string gender)
        //{
        //    if (age != 0 && !string.IsNullOrEmpty(gender))
        //    {
        //        HttpContext.Session.Set("age", age);
        //        HttpContext.Session.Set("gender", gender);
        //    }

        //    return RedirectToAction("Index");
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}








//public IActionResult Checkout()
//{
//    return View();
//}
//    }
//}
