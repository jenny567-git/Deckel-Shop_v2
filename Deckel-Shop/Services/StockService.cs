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

        public IEnumerable<Product> GetAllAvailableProducts()
        {
            return deckelShopContext.Products.Where(p => p.Status == "Available").AsEnumerable();
        }
        public IEnumerable<Product> GetAllRemovedProducts()
        {
            return deckelShopContext.Products.Where(p => p.Status == "Removed").AsEnumerable();
        }

        

        public Product GetProduct(int id)
        {
            return deckelShopContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            deckelShopContext.Products.Add(product);
            deckelShopContext.SaveChanges(); 
        }

        public void AddBackToStock(int id, int amount)
        {
            var product = deckelShopContext.Products.SingleOrDefault(p => p.Id == id);
            product.Status = "Available";
            product.Amount = amount;
            deckelShopContext.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
           var product = deckelShopContext.Products.SingleOrDefault(p => p.Id == id );
            product.Status = "Removed";
            deckelShopContext.SaveChanges();
        }

        public async Task <int> EditProduct(Product product)
        {
            var currentProduct = GetProduct(product.Id);
            
            deckelShopContext.Update(GetProduct(product.Id));
            deckelShopContext.SaveChanges();
            return await deckelShopContext.SaveChangesAsync();
        }


    }
}
