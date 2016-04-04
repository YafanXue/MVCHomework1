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
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index(CustsSearchViewModel CusSearch)
        {
            ViewBag.CusType = repoCustomer.GetCusType();
            var data = repoCustomer.All();
            if (!string.IsNullOrEmpty(CusSearch.Keyword) )
                data = data.Where(p => p.客戶名稱.Contains(CusSearch.Keyword) );
            if (CusSearch.客戶分類 != null)
                data = data.Where(p => p.客戶分類 == CusSearch.客戶分類);
            
            CusSearch.Customers = data.ToList() ;

            return View(CusSearch);
        }

        public ActionResult CusSummary()
        {
            var data=repoCusView.All();
            return View(data);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //var data = repoCustomer.Find(id.Value);
            var data = repoCustomer.GetCustomerContact(id.Value);
            if (data == null)
                return HttpNotFound();
            return View(data);
        }

        [HttpPost]
        public ActionResult Details(int? id,IList<UpdateBatchContact> data)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in data)
                {
                    var cus = repoContact.Find(item.Id);
                    cus.職稱 = item.職稱;
                    cus.手機 = item.手機;
                    cus.電話 = item.電話;
                }
                repoContact.UnitOfWork.Commit();
                TempData["BatchUpMsg"] = "批次更新客戶聯絡人成功";
            }
            else
                TempData["BatchUpMsg"] = "批次更新客戶聯絡人失敗";
            ViewData.Model = repoCustomer.GetCustomerContact(id.Value);

            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.CusType =repoCustomer.GetCusType();
            return View();
        }  

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                repoCustomer.Add(客戶);
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            
                return View(客戶);
            
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = repoCustomer.Find(id.Value);
            if (data == null)
                return HttpNotFound();
            ViewBag.CusType = repoCustomer.GetCusType();
            return View(data);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                var dbcus = repoCustomer.UnitOfWork.Context;
                dbcus.Entry(客戶).State = EntityState.Modified;
                repoCustomer.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
           
                return View(客戶);
            
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var data = repoCustomer.Find(id.Value);
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

                var target = repoCustomer.Find(id);
                if (target == null)
                    return HttpNotFound();
                repoCustomer.Delete(target);
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult ExcelExport()
        {
            return File( repoCustomer.GenerateDataTable(), "application/vnd.ms-excel","customers.xls");
        }
    }
}
