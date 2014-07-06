using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSaleSystem.Models
{
    public class Item
    {
        [Key] public int ItemId { get; set; }
        public string Description { get; set; }
        public string UPC { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public int Quantity { get; set; }
        public bool Taxable { get; set; }
    }
}