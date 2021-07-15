using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqTask.Models
{
    public class CustomerReport
    {
        public DateTime Date { get; set; }
        public Customer CustomerName { get; set; }
        public decimal? Amount { get; set; }
    }
}