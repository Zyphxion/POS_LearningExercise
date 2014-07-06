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
    public class InventoryController : Controller
    {
        private InventoryContext db = new InventoryContext();        

        // GET: /Inventory/
        public ActionResult Index()
        {
            Item_Category obj = new Item_Category();
            obj.categories = db.Categories.ToList();
            obj.items = db.Items.ToList();
            return View(obj);
        }

        // GET
        public ActionResult Search(string SearchBox)
        {
            var items = (from i in db.Items
                        where i.Description.Contains(SearchBox)
                        || i.UPC.Contains(SearchBox)
                        select i).ToList();

            Item_Category obj = new Item_Category();
            obj.categories = db.Categories.ToList();
            obj.items = items;
            return View("Index", obj);
        }


        // GET
        public ActionResult CreateSearch(string SearchBox)
        {
            var items = (from i in db.Items
                         where i.UPC.Contains(SearchBox)
                         select i).ToList();

            ViewBag.Categories = db.Categories.ToList();
            if (items.Any())
            {
                return RedirectToAction("Edit", new { id = items[0].ItemId });
            }
            return RedirectToAction("Create");
        }


        // GET: /Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(item);
        }

        // GET: /Inventory/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        // POST: /Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ItemId,Description,UPC,CategoryId,Price,Cost,Quantity,Taxable")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(item);
        }

        // GET: /Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(item);
        }

        // POST: /Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ItemId,Description,UPC,CategoryId,Price,Cost,Quantity,Taxable")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(item);
        }

        // GET: /Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
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
