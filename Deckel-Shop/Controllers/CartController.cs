using Database.Models;
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
            Id = 27,
            CustomerId = customer.Id,
            CustomerName = customer.FirstName,
            Products = productlist
        };

        [HttpGet]
        public IActionResult Index()
        {
            var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");
            if (shopCart != null)
            {
                productlist = shopCart.Products;
            }
            return View("SavedCart", shopCart);
        }

        //public IActionResult kjhslkijgfhlisdfujhgidslfuhdilfuhdifubhdifugbh()
        //{

        //    var shopCart = SessionHelper.Get<Cart>(HttpContext.Session, "cart");

        //    if (shopCart != null)
        //    {
        //        products = shopCart.Products;
        //    }

        //    return View(products);
        //}

        [HttpPost]
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

        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }

        [HttpPost]
        public ActionResult AddOrder(Customer detail, OrderedItem order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (AddOrderBool(detail, order))
                    {
                        ViewBag.Message = "Order added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public bool AddOrderBool(Customer obj, OrderedItem obj1)
        {

            connection();
            SqlCommand com = new SqlCommand("AddOrderDetail", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@FirstName", obj.FirstName);
            com.Parameters.AddWithValue("@LastName", obj.LastName);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@Country", obj.Country);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Street);
            com.Parameters.AddWithValue("@ZipCode", obj.ZipCode);
            com.Parameters.AddWithValue("@OrderId", obj1.OrderId);
            com.Parameters.AddWithValue("@ProductId", obj1.ProductId);
            com.Parameters.AddWithValue("@Amount", obj1.Amount);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }
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








//public IActionResult Checkout()
//{
//    return View();
//}
//    }
//}
