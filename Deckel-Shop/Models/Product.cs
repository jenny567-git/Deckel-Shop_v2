﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Deckel_Shop.Models
{
    public class Product
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Description { get; set; }

        public int Amount { get; set; }

        public string Photo
        {
            get;
            set;
        }
    }
}
