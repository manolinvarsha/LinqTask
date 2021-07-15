using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqTask.Models
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public Product ProductName { get; set; }
        public decimal? Quantity { get; set; }
    }
}