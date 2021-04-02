using System;
using System.Collections.Generic;

#nullable disable

namespace Database.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderedItems = new HashSet<OrderedItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string ImgName { get; set; }

        public string ImgName2 { get; set; }
        public string Category { get; set; }

        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
