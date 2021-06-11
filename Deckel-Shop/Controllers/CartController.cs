using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Deckel_Shop.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
//using Stripe;
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
        private readonly Services.OrderService orderService = new Services.OrderService();
        private readonly Services.CustomerService _cs = new Services.CustomerService();

        [TempData]
        public string TotalAmount { get; set; }
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
                    new Database.Models.Product
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

            vm.Customer = (shopCart.Customer == null) ? new Database.Models.Customer() : shopCart.Customer;

            return View(vm);
        }


        //METHOD WITH NOT WORKING STRIPE

        //[HttpPost]
        //public ActionResult AddOrder(string stripeToken, string stripeEmail, [Bind] CheckoutViewModel vm)
        //{

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Database.Models.Order order = new Database.Models.Order();

        //            order.CustomerId = _cs.GetCustomerId(vm.Customer.Email);
        //            if (order.CustomerId == 0)
        //            {
        //                order.Customer = vm.Customer;
        //            }

        //            order.OrderStatus = "Pending";
        //            order.OrderDate = DateTime.Now;
        //            order.ShippingDate = DateTime.UnixEpoch;

        //            var cart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
        //            foreach (var product in cart.Products)
        //            {
        //                OrderedItem orderedItem = new OrderedItem();
        //                orderedItem.ProductId = product.Id;
        //                orderedItem.Amount = product.Amount;
        //                order.OrderedItems.Add(orderedItem);

        //            }

        //            Debug.WriteLine(cart.TotalPrice);
        //            order.OrderTotal = cart.TotalPrice;



        //            var optionsCustomer = new CustomerCreateOptions
        //            {
        //                Email = stripeEmail,
        //                Name = order.Customer.FirstName + " " + order.Customer.LastName,
        //                Phone = order.Customer.Phone,
        //            };
        //            var serviceCustomer = new Stripe.CustomerService();
        //            Stripe.Customer customer = serviceCustomer.Create(optionsCustomer);
        //            var optionsCharge = new ChargeCreateOptions
        //            {
        //                Amount = Convert.ToInt64(cart.TotalPrice),
        //                Currency = "SEK",
        //                Description = "selling caps",
        //                Source = stripeToken,
        //                ReceiptEmail = stripeEmail
        //            };
        //            var serviceCharge = new ChargeService();
        //            Charge charge = serviceCharge.Create(optionsCharge);






        //            orderService.AddOrder(order);
        //            _ss.UpdateStockWhenPlacingOrder(order);
        //            HttpContext.Session.Clear();

        //            if (charge.Status == "succeeded")
        //            {
        //                return RedirectToAction(nameof(OrderConfirmation));
        //            }
        //            else
        //            {
        //                return RedirectToAction(nameof(Failed));
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    throw new NotImplementedException();
        //}

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
                    _ss.UpdateStockWhenPlacingOrder(order);
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
        
        public IActionResult Failed()
        {
            return View();
        }
    }
}





