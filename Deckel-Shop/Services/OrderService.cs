using Database.Models;
using Deckel_Shop.Models;
using Microsoft.EntityFrameworkCore;
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
            if (status != null)
            {
                return deckelShopContext.Orders.Where(o => o.OrderStatus == status).Include(c => c.Customer).AsEnumerable();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public IEnumerable<Order> GetAllOrdersBySelectedCustomer(int id)
        {
            return deckelShopContext.Orders.Where(o => o.CustomerId == id);
        }

        public Order GetOrder(int id)
        {

            int custid = deckelShopContext.Orders.FirstOrDefault(o => o.OrderId == id).CustomerId;

            CustomerService cs = new CustomerService();

            //Customer cust = cs.GetCustomer(custid);

            Order order = new Order();

            //var viewModel = CreateOrderViewModel(order, cust, order.OrderedItems.ToList());


            order = deckelShopContext.Orders.Include(c => c.Customer).Include(o => o.OrderedItems).ThenInclude(p => p.Product).SingleOrDefault(o => o.OrderId == id);


            return order;
        }

        public void AddOrder(Order order)
        {
            deckelShopContext.Orders.Add(order);
            deckelShopContext.SaveChanges();
            int a = 0;
        }

        public async Task<int> RemoveOrder(int id)
        {
            GetOrder(id).OrderedItems.Clear();
            deckelShopContext.Orders.Remove(GetOrder(id));
           return await deckelShopContext.SaveChangesAsync();
        }

        //public void SendOrder(int id)
        //{
        //    var currentOrder = deckelShopContext.Orders.FirstOrDefault(x => x.OrderId == id);
        //    if (currentOrder != null)
        //    {
        //        //change orderstatus to int???
        //        currentOrder.OrderStatus = "Delivered";
        //    }
        //    deckelShopContext.SaveChanges();
        //}


        public async Task<int> SendOrder(int id)
        {
            Order o = GetOrder(id);
            o.OrderStatus = "Delivered";
            o.ShippingDate = DateTime.Now;
            deckelShopContext.Update(GetOrder(id));

            return await deckelShopContext.SaveChangesAsync();
        }

    }
}
