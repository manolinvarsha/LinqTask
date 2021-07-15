using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqTask.Models;

namespace LinqTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (dbPurchaseEntities db = new dbPurchaseEntities())
            {
                List<Product> products = db.Products.ToList();
                List<Customer> customers = db.Customers.ToList();
                List<PurchaseOrder> purchaseOrders = db.PurchaseOrders.ToList();
                List<PurchaseOrderDetail> purchaseOrderDetails = db.PurchaseOrderDetails.ToList();
                var listOfPurchaseOrder = (from c in customers
                                           join po in purchaseOrders on c.CustomerID equals po.CustomerID
                                           join pod in purchaseOrderDetails on po.POID equals pod.POID
                                           join p in products on pod.ProductID equals p.ProductID
                                           select new ListOfPurchaseOrders
                                           {
                                               customer = c,
                                               purchaseOrder = po,
                                               purchaseOrderDetail = pod,
                                               product = p
                                           });
                return View(listOfPurchaseOrder);
            }
        }
        public ActionResult CustomerReport()
        {
            using (dbPurchaseEntities db = new dbPurchaseEntities())
            {
                List<Customer> customers = db.Customers.ToList();
                List<PurchaseOrder> purchaseOrders = db.PurchaseOrders.ToList();
                var customerReport = (from c in customers
                                      join po in purchaseOrders on c.CustomerID equals po.CustomerID
                                      group new { c, po } by new { po.Date.Value.Month } into G
                                      let firstproductgroup = G.FirstOrDefault()
                                      let DOP = firstproductgroup.po
                                      let CustomerName = firstproductgroup.c
                                      let maxamount = G.Sum(m => m.po.Amount)
                                      select new CustomerReport
                                      {
                                          Date = (DateTime)DOP.Date,
                                          CustomerName=CustomerName,
                                          Amount = maxamount
                                      });
                return View(customerReport);
            }
        }
        public ActionResult SalesReport()
        {
            using (dbPurchaseEntities db = new dbPurchaseEntities())
            {
                List<Product> products = db.Products.ToList();
                List<PurchaseOrder> purchaseOrders = db.PurchaseOrders.ToList();
                List<PurchaseOrderDetail> purchaseOrderDetails = db.PurchaseOrderDetails.ToList();
                var salesReport = (from po in purchaseOrders
                                   join pod in purchaseOrderDetails on po.POID equals pod.POID
                                   join p in products on pod.ProductID equals p.ProductID
                                   group new { p,po, pod } by new { po.Date.Value.Month } into G
                                   let firstproductgroup = G.FirstOrDefault()
                                   let DOP = firstproductgroup.po
                                   let ProductName = firstproductgroup.p
                                   let quantity = G.Sum(m => m.pod.Quantity)
                                   select new SalesReport
                                   {
                                       Date = (DateTime)DOP.Date,
                                       ProductName = ProductName,
                                       Quantity = quantity
                                   });
                return View(salesReport);
            }
        }
           
        
        public ActionResult ProductNameList()
        {
            using (dbPurchaseEntities db = new dbPurchaseEntities())
            {
                List<Product> products = db.Products.ToList();
                var productNameAsc = from p in products
                                      orderby p.ProductName
                                      select new ListOfPurchaseOrders
                                      {
                                          product = p
                                      };
                return View(productNameAsc);
             }
        }
        public ActionResult MaximumPrice()
        {
            using (dbPurchaseEntities db = new dbPurchaseEntities())
            {
                List<PurchaseOrder> purchaseOrder = db.PurchaseOrders.ToList();
                List<PurchaseOrderDetail> purchaseOrderDetail = db.PurchaseOrderDetails.ToList();
                var Maximum = (from po in purchaseOrder
                               join pod in purchaseOrderDetail on po.POID equals pod.POID
                               group new { po, pod } by new { po.Date.Value.Month } into G
                               let firstproductgroup = G.FirstOrDefault()
                               let DOP = firstproductgroup.po
                               let POID = firstproductgroup.pod
                               let maxprice = G.Max(m => m.pod.Price)
                               select new Monthwisemaxprice
                               {
                                   Date = (DateTime)DOP.Date,
                                   POID = POID,
                                   Price = maxprice
                               });
                return View(Maximum);
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}