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
        public Test Get()
        {
            Test t = new Test();
            t.Id = 3434;
            return t;
        }
        
        [HttpPost]
        public OrderViewModel Post(Test id)
        {
            var os = new OrderService();
            var cs = new CustomerService();
            var order = os.GetOrder(id.Id);
            var customer = cs.GetCustomer(order.CustomerId);
            var orderedItems = order.OrderedItems;

            OrderViewModel viewModel = new OrderViewModel(order, customer, orderedItems);
            
            return viewModel;
        }
    }

    public class Test
    {
        public int Id { get; set; }
    }
}
