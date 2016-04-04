using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticeCustomer.Models
{
    public class CustsContactViewModel
    {
        public 客戶資料 Customer { get; set; }

        //public ContactViewModel Contact { get; set; }
        public IEnumerable<客戶聯絡人> Contacts { get; set; }
        //public IEnumerable<ContactViewModel> Contacts { get; set; }
    }
}