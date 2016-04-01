using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ParticeCustomer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All() 
        {
            return base.All().Where(p=>p.已刪除== false);
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶銀行資訊> Search(string keyword)
        {
            return this.All().Where(p => p.帳戶名稱.Contains(keyword) || p.銀行名稱.Contains(keyword));
        }

        public List<SelectListItem> GetCustomerList()
        {
            var custs= RepositoryHelper.Get客戶資料Repository().All();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var cus in custs)
            {
                items.Add(new SelectListItem() { Text = cus.客戶名稱, Value = cus.Id.ToString() });
            }
            return items;
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.已刪除 = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}