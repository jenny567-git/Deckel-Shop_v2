using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Deckel_Shop.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly StockService _ss = new StockService();
        private readonly OrderService orderService = new OrderService();
        private readonly CustomerService _cs = new CustomerService();


        public IActionResult Index()
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
           
            return View("SavedCart", shopCart);
        }

        public IActionResult AddProductToCart(int id)
        {
            var productInStock = _ss.GetProduct(id);

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), "product");
            }

            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

            if (shopCart == null)
            {
               
                shopCart = new Cart();
            }

            if (shopCart.Products.Exists(p => p.Id == productInStock.Id))
            {
                var itemInCart = shopCart.Products.FirstOrDefault(p => p.Id == productInStock.Id);
                if (productInStock.Amount > itemInCart.Amount)
                {
                    ++itemInCart.Amount;
                }

            }
            else
            {
                shopCart.Products.Add(
                    new Product
                    {
                        Id = productInStock.Id,
                        Name = productInStock.Name,
                        ImgName = productInStock.ImgName,
                        ImgName2 = productInStock.ImgName2,
                        Description = productInStock.Description,
                        Price = productInStock.Price,
                        Amount = 1,
                        Category = productInStock.Category,
                        Status = productInStock.Status
                    });
            }

            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);

            return RedirectToAction(nameof(Index), "product");
        }

        public IActionResult RemoveProduct(int id)
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            shopCart.Products.RemoveAll(p => p.Id == id);
            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);
            TempData["msg"] = "Item has been delete from cart";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeQuantity(int id, int amount)
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            var product = shopCart.Products.Find(p => p.Id == id);
            if (_ss.GetProduct(id).Amount >= amount)
            {
                product.Amount = amount;
                TempData["msg"] = "Quantity has been updated.";
            }
            else
            {
                TempData["msg"] = "Unable to update quantity due to low stock";
            }

            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout(decimal totalPrice)
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            shopCart.TotalPrice = totalPrice;
            SessionHelper.Set<Cart>(HttpContext.Session, "cart", shopCart);

            CheckoutViewModel vm = new CheckoutViewModel();
            vm.Products = shopCart.Products;
            vm.TotalPrice = shopCart.TotalPrice;

            vm.Customer = (shopCart.Customer == null) ? new Customer() : shopCart.Customer;

            return View(vm);
        }



        [HttpPost]
        public ActionResult AddOrder([Bind] CheckoutViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Order order = new Order();

                    order.CustomerId = _cs.GetCustomerId(vm.Customer.Email);
                    if (order.CustomerId == 0)
                    {
                        order.Customer = vm.Customer;
                    }

                    order.OrderStatus = "Pending";
                    order.OrderDate = DateTime.Now;
                    order.ShippingDate = DateTime.UnixEpoch;

                    var cart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
                    foreach (var product in cart.Products)
                    {
                        OrderedItem orderedItem = new OrderedItem();
                        orderedItem.ProductId = product.Id;
                        orderedItem.Amount = product.Amount;
                        order.OrderedItems.Add(orderedItem);

                    }

                    Debug.WriteLine(cart.TotalPrice);
                    order.OrderTotal = cart.TotalPrice;

                    orderService.AddOrder(order);
                    StockService stockService = new StockService();
                    stockService.UpdateStockWhenPlacingOrder(order);
                    HttpContext.Session.Clear();
                    
                }


            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction(nameof(OrderConfirmation));
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}





