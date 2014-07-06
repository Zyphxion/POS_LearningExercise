using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSaleSystem.Models;
using PointOfSaleSystem.ViewModels;

namespace PointOfSaleSystem.Controllers
{
    public class RegisterController : Controller
    {
        private InventoryContext db = new InventoryContext();
        private static List<Item> items = new List<Item>();
        private static List<Sale> sales = new List<Sale>();
        private static Guid transactionId = System.Guid.NewGuid();

        // GET: /Register/
        public ActionResult Index()
        {
            ViewBag.TransId = transactionId.ToString();
            Item_Sale obj = new Item_Sale();
            obj.sales = sales;
            obj.items = items;
            return View(obj);
        }

        // GET
        public ActionResult Search(string SearchBox)
        {
            var itemsfromdb = (from i in db.Items
                         where i.UPC.Contains(SearchBox)
                         select i).ToList();

            if (itemsfromdb.Any())
            {
                Sale checksale = sales.Find(itemid => itemid.ItemId == itemsfromdb[0].ItemId);
                if (checksale != null)
                {
                    checksale.AmountSold += 1;
                }
                else
                {
                    items.Add(itemsfromdb[0]);
                    Sale newsale = new Sale()
                    {
                        TransactionId = transactionId.ToString(),
                        AmountSold = 1,
                        SaleDate = DateTime.Now,
                        ItemId = items.Last().ItemId,
                        Item = items.Last()
                    };
                    sales.Add(newsale);
                }
            }

            Item_Sale obj = new Item_Sale();
            obj.sales = sales;
            obj.items = items;

            return RedirectToAction("Index", obj);
        }

        public ActionResult Pay(decimal? Payment)
        {
            decimal total = 00.00m;
            if (sales.Any()) {
                foreach (Sale sale in sales){
                    if (sale.AmountSold > 0)
                    {
                        if (sale.Item.Taxable)
                        {
                            total += Convert.ToDecimal(sale.Item.Price) + (Convert.ToDecimal(sale.Item.Price) * 0.09m);
                        }
                        else
                        {
                            total += Convert.ToDecimal(sale.Item.Price);
                        }
                        sale.Item.Quantity -= sale.AmountSold;
                        db.Entry(sale.Item).State = EntityState.Modified;
                        db.SaveChanges();
                        sale.Item = null;
                        db.Sales.Add(sale);
                        db.SaveChanges();
                    }
                }
                items = new List<Item>();
                sales = new List<Sale>();
                transactionId = System.Guid.NewGuid();

                if (Payment == null)
                {
                    return View();
                }
                decimal change = Convert.ToDecimal(Payment) - total;
                return View(change);
            }
            return RedirectToAction("Index");
        }

        //
        public ActionResult AddQuant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale checksale = sales.Find(itemid => itemid.ItemId == id);
            if (checksale != null)
            {
                checksale.AmountSold += 1;
            }

            if (checksale == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        //
        public ActionResult SubQuant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale checksale = sales.Find(itemid => itemid.ItemId == id);
            if (checksale != null)
            {
                if (checksale.AmountSold > 0)
                {
                    checksale.AmountSold -= 1;
                }
            }

            if (checksale == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // GET: /Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = items.Find(itemid => itemid.ItemId == id);
            Sale sale = sales.Find(itemid => itemid.ItemId == id);
            if (item == null || sale == null)
            {
                return HttpNotFound();
            }
            sales.Remove(sale);
            items.Remove(item);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
