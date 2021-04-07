using Database.Models;
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
        public IActionResult Index()
        {
            return View();
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

    }

    
    
}
