using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using ParticeCustomer.Controllers;

namespace ParticeCustomer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{

        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.已刪除 == false) ;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.已刪除 = true;
           // base.Delete(entity);
        }

        public Stream GenerateDataTable()
        {
            var data = RepositoryHelper.GetContactExcelViewRepository();

            return NPOIExcel.RenderListToExcel(data.All().ToList());
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}