using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSaleSystem.Models
{
    public class Sale
    {

        [Key] public int SaleId { get; set; }
        public string TransactionId { get; set; }
        public int ItemId { get; set; }
        public int AmountSold { get; set; }
        public DateTime SaleDate { get; set; }
        public virtual Item Item { get; set; }
    }
}