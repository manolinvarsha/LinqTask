using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqTask.Models
{
    public class Monthwisemaxprice
    {
     
            public DateTime Date { get; set; }
            public  PurchaseOrderDetail POID { get; set; }
            public decimal? Price { get; set; }
        
    }
}