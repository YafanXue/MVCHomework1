using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParticeCustomer.Models;
using System.ComponentModel.DataAnnotations;

namespace ParticeCustomer.Controllers
{
    public class ContactController : BaseController
    {        
        // GET: Contact
        public ActionResult Index(string Keyword)
        {
            var data = repoContact.All();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = repoContact.All().Where(p => p.姓名.Contains(Keyword));
            }            
            return View(data);

        }



        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            
            if (ModelState.IsValid)
            {
                repoContact.Add(客戶聯絡人);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db客戶聯絡人 = repoContact.UnitOfWork.Context;
                db客戶聯絡人.Entry(客戶聯絡人).State = EntityState.Modified;
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id);
            repoContact.Delete(客戶聯絡人);
            repoContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //public ActionResult SearchContract(FormCollection collection)
        //{
        //    var data = db.Database.SqlQuery<客戶聯絡人>(@"select * from dbo.客戶聯絡人 where 姓名 like @p0 
        //             ", string.Format("%{0}%",collection["myText"]));
        //    return View("Index",data);
        //}
        public ActionResult ExcelExport()
        {
            return File(repoContact.GenerateDataTable(), "application/vnd.ms-excel", "contacts.xls");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoContact.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
