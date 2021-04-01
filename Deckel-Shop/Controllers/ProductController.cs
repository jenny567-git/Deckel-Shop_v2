using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(Product product)
        {
            string mycmd = "select product.Id,product.Name,product.Price,product.Description,product.Amount,product.ImgName";
            var dt = new DataTable();

            


            List<Product> list = new List<Product>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product mob = new Product();
                mob.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                mob.Name = dt.Rows[i]["Name"].ToString();
                mob.Price = (decimal)Convert.ToDouble(dt.Rows[i]["Price"]);
                mob.Description = dt.Rows[i]["Description"].ToString();
                mob.Amount = Convert.ToInt32(dt.Rows[i]["Amount"].ToString());
                mob.ImgName = dt.Rows[i]["ImgName"].ToString();
                

                list.Add(mob);
            }
            return View(list);
        }
    }
}
