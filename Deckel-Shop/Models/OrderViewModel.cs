using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal OrderTotal { get; set; }

        public Customer Customer { get; set; }
        public List<OrderedItem> OrderedItems { get; set; }


        public OrderViewModel(Order order, Customer customer, ICollection<OrderedItem> orderedItems)
        {
            OrderId = order.OrderId;
            CustomerId = order.CustomerId;
            OrderStatus = order.OrderStatus;
            OrderDate = order.OrderDate;
            ShippingDate = order.ShippingDate;
            OrderTotal = order.OrderTotal;


            Customer = customer;
            OrderedItems = orderedItems.ToList();
        }
    }
}