using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ParticeCustomer.Models
{   
	public  class BanksExcelViewRepository : EFRepository<BanksExcelView>, IBanksExcelViewRepository
	{

	}

	public  interface IBanksExcelViewRepository : IRepository<BanksExcelView>
	{

	}
}