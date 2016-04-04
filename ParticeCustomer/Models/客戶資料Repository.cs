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

        public CustsContactViewModel GetCustomerContact(int id)
        {
            CustsContactViewModel data=new CustsContactViewModel();
            data.Customer = this.Find(id);
            var dbcontacts = RepositoryHelper.Get客戶聯絡人Repository();
            var tempdata= dbcontacts.All().Where(p=>p.客戶Id== id);
            List<ContactViewModel> contacts = new List<ContactViewModel>();
                 
            foreach (var item in tempdata)
            {

                //contacts.Concat(new[] {new ContactViewModel {Id= item.Id,
                //CusId=item.客戶Id,姓名=item.姓名,Title=item.職稱,Mobile=item.手機,
                //TEL=item.電話,已刪除= item.已刪除,Email = item.Email} });
                var target = new ContactViewModel();
                target.Id = item.Id;
                target.CusId = item.客戶Id;
                target.Email = item.Email;
                target.Mobile = item.手機;
                target.TEL = item.電話;
                target.Title = item.職稱;
                target.姓名 = item.姓名;
                target.已刪除 = item.已刪除;
                contacts.Add(target);


            }
            data.Contacts=contacts;
            return data;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}