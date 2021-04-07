using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }

        public double TotalPrice { get; set; }
    }
}
