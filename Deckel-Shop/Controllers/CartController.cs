﻿using Database.Models;
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
            Customer = customer,
            Products = productlist
        };

        //public IActionResult kjhslkijgfhlisdfujhgidslfuhdilfuhdifubhdifugbh()
        //{

        //    var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

        //    if (shopCart != null)
        //    {
        //        products = shopCart.Products;
        //    }

        //    return View(products);
        //}

        public IActionResult Index()
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            if (shopCart != null)
            {
                productlist = shopCart.Products;
            }
            return View("SavedCart", shopCart);
        }

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
                    Customer = cart.Customer,
                    TotalPrice = cart.TotalPrice,
                    Products = new List<Product>()
                };
            }

            if (shopCart.Products.Exists(p => p.Id == product.Id))
            {
                var itemInCart = shopCart.Products.FirstOrDefault(p => p.Id == product.Id);
                if(_ss.GetProduct(id).Amount > itemInCart.Amount)
                {
                ++shopCart.Products.First(p => p.Id == product.Id).Amount;
                }
                
            }
            else
            {
                shopCart.Products.Add(
                    new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        ImgName2 = product.ImgName2,
                        Description = product.Description,
                        Price = product.Price,
                        Amount = 1
                    });
            }

            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);

            return RedirectToAction(nameof(Index), "product");
        }

        public IActionResult RemoveProduct(int id)
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            //var product = shopCart.Products.Single(p => p.Id == id);
            shopCart.Products.RemoveAll(p => p.Id == id);
            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeQuantity(int id, int amount)
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            var product = shopCart.Products.Find(p => p.Id == id);
            if (_ss.GetProduct(id).Amount >= amount)
            {
                product.Amount = amount;

            }

            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);
            //shopCart.Products.Update(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {
            return View();
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

