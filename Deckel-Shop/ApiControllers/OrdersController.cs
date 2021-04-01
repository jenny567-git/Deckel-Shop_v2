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
        public Test Post(Test id)
        {
            id.Id = 23123;
            return id;
        }
    }

    public class Test
    {
        public int Id { get; set; }
    }
}
