﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.WebUI.Models
{
    [Table("Shop")]
    public class Shop : Sale
    {
        public Shop()
        {
        }

        public short? MaxQuantity { get; set; }
    }
}