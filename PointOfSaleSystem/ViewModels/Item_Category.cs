using PointOfSaleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSaleSystem.ViewModels
{
    public class Item_Category
    {
        public List<Category> categories { get; set; }
        public List<Item> items { get; set; }
    }
}