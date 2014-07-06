using PointOfSaleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSaleSystem.ViewModels
{
    public class Transaction
    {
        public List<Sale> sales { get; set; }
        public string transactionId { get; set; }
        public decimal totalprice { get; set; }
        public decimal totalcost { get; set; }
    }
}