using ParticeCustomer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ParticeCustomer.Controllers
{
    public class CustomerController : Controller
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: Customer
        public ActionResult Index(string Keyword)
        {
            var data = db.客戶資料.Where(p => p.已刪除 == false).ToList();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = db.客戶資料.Where(p => p.客戶名稱.Contains(Keyword)).ToList();
            }
            return View(data);
        }

        public ActionResult CusSummary()
        {
            var data=db.CUSView.ToList();
            return View(data);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = db.客戶資料.Find(id);
            if (data == null)
                return HttpNotFound();
            return View(data);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                客戶.已刪除 = false;
                db.客戶資料.Add(客戶);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
                return View(客戶);
            
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data=db.客戶資料.Find(id);
            if (data == null)
                return HttpNotFound();
            return View(data);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                客戶.已刪除 = false;
                客戶.Id = id;
                db.Entry(客戶).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
                return View(客戶);
            
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = db.客戶資料.Find(id);
            return View(data);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //var target = db.客戶資料.Find(id);
                //db.客戶資料.Remove(target);

                var target = db.客戶資料.FirstOrDefault(p => p.Id == id);
                if (target == null)
                    return HttpNotFound();
                target.已刪除 = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
