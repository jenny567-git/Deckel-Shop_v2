using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Database.Models
{
    public partial class OrderedItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
