using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Services
{
    public class StockService
    {
        private readonly DeckelShopContext deckelShopContext;
        public StockService()
        {
            deckelShopContext = new DeckelShopContext();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return deckelShopContext.Products.AsEnumerable();
        }
        public IEnumerable<Product> GetProduct(int id)
        {
            return deckelShopContext.Products.Where(p => p.Id == id).AsEnumerable();
        }

        public void AddProduct(Product product)
        {
            deckelShopContext.Products.Add(product);
            deckelShopContext.SaveChanges(); 
        }

        public void RemoveProduct(Product product)
        {
            deckelShopContext.Products.Remove(product);
            deckelShopContext.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            var currentProduct = deckelShopContext.Products.FirstOrDefault(x => x.Id == product.Id);
            if (currentProduct != null)
            {
                currentProduct = product;
            }
            deckelShopContext.SaveChanges();
        }


    }
}
