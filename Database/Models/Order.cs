using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Database.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderedItems = new HashSet<OrderedItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal OrderTotal { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
