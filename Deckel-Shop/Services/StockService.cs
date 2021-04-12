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
        private readonly OrderService _os;

        public StockService()
        {
            deckelShopContext = new DeckelShopContext();
            _os = new OrderService();
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

        public void UpdateStockWhenCancelledOrder(int orderId)
        {
            var order = _os.GetOrder(orderId);
            foreach (var item in order.OrderedItems)
            {
                deckelShopContext.Products.SingleOrDefault(p => p.Id == item.ProductId).Amount += item.Amount;
            }
            deckelShopContext.SaveChanges();
        }

        public void UpdateStockWhenPlacingOrder(Order order)
        {
            foreach (var item in order.OrderedItems)
            {
                deckelShopContext.Products.SingleOrDefault(p => p.Id == item.ProductId).Amount -= item.Amount;
            }
            deckelShopContext.SaveChanges();
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
        public void UpdateStock(int id, int amount)
        {
            var product = deckelShopContext.Products.SingleOrDefault(p => p.Id == id);
            product.Amount += amount;
            deckelShopContext.SaveChanges();
        }
        public int GetStockStatusByProductId(int id)
        {
            var product = deckelShopContext.Products.SingleOrDefault(p => p.Id == id);
            return product.Amount;
        }

        public void RemoveProduct(int id)
        {
           var product = deckelShopContext.Products.SingleOrDefault(p => p.Id == id );
            product.Status = "Removed";
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
