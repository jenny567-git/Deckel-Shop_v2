using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Services
{
    public class OrderService
    {
        private readonly DeckelShopContext deckelShopContext;

        public OrderService()
        {
            deckelShopContext = new DeckelShopContext();
        }

        // info - delete - sendOrder


        public IEnumerable<Order> GetAllOrdersByOrderStatus(string status)
        {
            if (status != null) { 
            return deckelShopContext.Orders.Where(o=> o.OrderStatus == status).AsEnumerable();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Order> GetOrder(int id)
        {
            return deckelShopContext.Orders.Where(o => o.OrderId == id).AsEnumerable();
        }

        public void AddOrder(Order order)
        {
            deckelShopContext.Orders.Add(order);
            deckelShopContext.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            deckelShopContext.Orders.Remove(order);
            deckelShopContext.SaveChanges();
        }

        public void SendOrder (Order order)
        {
            var currentOrder = deckelShopContext.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (currentOrder != null)
            {
                //change orderstatus to int???
                currentOrder.OrderStatus = "Shipped";
            }
            deckelShopContext.SaveChanges();
        }

    }
}
