using Database.Models;
using Deckel_Shop.Models;
using Deckel_Shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        [Route("orderDetails")]
        [HttpPost]
        public OrderViewModel orderDetails(InputData data)
        {
            var os = new OrderService();
            var cs = new CustomerService();
            var order = os.GetOrder(data.Id);
            var customer = cs.GetCustomer(order.CustomerId);
            var orderedItems = order.OrderedItems;

            OrderViewModel viewModel = new OrderViewModel(order, customer, orderedItems);

            return viewModel;
        }


        [Route("customerDetails")]
        [HttpPost]
        public Customer customerDetails(InputData data)
        {
            var cs = new CustomerService();
            var customer = cs.GetCustomer(data.Id);
            
            return customer;
        }
        
        [Route("stockDetails")]
        [HttpPost]
        public Product stockDetails(InputData data)
        {
            var ss = new StockService();
            var product = ss.GetProduct(data.Id);
            
            return product;
        }

    }

    public class InputData
    {
        public int Id { get; set; }
    }
}
