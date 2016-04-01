using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer.Models
{   
	public  class CustsExcelViewRepository : EFRepository<CustsExcelView>, ICustsExcelViewRepository
	{

	}

	public  interface ICustsExcelViewRepository : IRepository<CustsExcelView>
	{

	}
}