using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiHarjoituskoodi_1.Models
{
    public partial class ProductsDailySale
    {
        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public double? DailySales { get; set; }
    }
}
