using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using ParticeCustomer.Controllers;
using System.IO;
using System.Web.Mvc;

namespace ParticeCustomer.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.已刪除 == false);
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.已刪除 = true;
        }

        public Stream GenerateDataTable()
        {
            var data = RepositoryHelper.GetCustsExcelViewRepository();

            return NPOIExcel.RenderListToExcel(data.All().ToList());


        }



        public List<SelectListItem> GetCusType()
        {
            var data=RepositoryHelper.Get客戶分類資訊Repository();
            List<SelectListItem> typeitems = new List<SelectListItem>();
            typeitems.Add(new SelectListItem() { Text ="請選擇", Value ="" });
            foreach (客戶分類資訊 item in data.All())
            {
                typeitems.Add(new SelectListItem() { Text = item.分類名稱, Value = item.Id.ToString() });
            }
            return typeitems;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}