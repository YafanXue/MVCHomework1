using ParticeCustomer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ParticeCustomer.Controllers
{
    public class BankController : BaseController
    {
        // GET: Back
        public ActionResult Index(string Keyword)
        {
            var data = repoBank.All();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = repoBank.Search(Keyword);
            }

            return View(data);

        }

        // GET: Back/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = repoBank.Find(id.Value);
            if (data == null)
                return new HttpNotFoundResult("no this detail");
            return View(data);
        }

        // GET: Back/Create
        public ActionResult Create()
        {
            //var custs = db.客戶資料.ToList();
            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach(var cus in custs)
            //{
            //    items.Add(new SelectListItem() { Text = cus.客戶名稱, Value = cus.Id.ToString() });
            //}
            var items=repoBank.GetCustomerList();
            ViewBag.CustomerList = items;
            return View();
        }

        // POST: Back/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 銀行)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                repoBank.Add(銀行);
                repoBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            
            
                return View(銀行);
            
        }

        // GET: Back/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = repoBank.Find(id.Value);
            if (data == null)
                return HttpNotFound();
            return View(data);
        }

        // POST: Back/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 銀行)
        {
            if(ModelState.IsValid)
            {
                // TODO: Add update logic here
                var db銀行 = repoBank.UnitOfWork.Context;
                db銀行.Entry(銀行).State = EntityState.Modified;
                repoBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            
                return View(銀行);
            
        }

        // GET: Back/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = repoBank.Find(id.Value);
            if (data == null)
                return HttpNotFound();
            return View(data);
        }

        // POST: Back/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, [Bind(Include = "客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 銀行)
        {
            try
            {
                // TODO: Add delete logic here
                客戶銀行資訊 bankdata = repoBank.Find(id);
                repoBank.Delete(bankdata);
                repoBank.UnitOfWork.Commit();
                
            }
            catch(DbEntityValidationException ex)
            {
                foreach(var ent in ex.EntityValidationErrors)
                {
                    foreach(var error in ent.ValidationErrors)
                    {
                        return Content(string.Format("{0}{1}", error.PropertyName, error.ErrorMessage));
                    }
                }
            }
            return RedirectToAction("Index");

        }

        public ActionResult ExcelExport()
        {
            return File(repoBank.GenerateDataTable(), "application/vnd.ms-excel", "banks.xls");
        }
    }
}
