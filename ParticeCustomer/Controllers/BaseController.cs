using ParticeCustomer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParticeCustomer.Controllers
{
    public class BaseController : Controller
    {
        protected 客戶聯絡人Repository repoContact = RepositoryHelper.Get客戶聯絡人Repository();
        protected 客戶銀行資訊Repository repoBank = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶資料Repository repoCustomer = RepositoryHelper.Get客戶資料Repository();
        protected CUSViewRepository repoCusView = RepositoryHelper.GetCUSViewRepository();

        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
        }
    }
}