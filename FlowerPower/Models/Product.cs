﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerPower.Models
{
    public class Product
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public double Price
        {
            get; set;
        }

        public string Photo
        {
            get; set;
        }
    }
}