using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticeCustomer.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public int CusId { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string Title { get; set; }
        public string 姓名 { get; set; }
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string Mobile { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string TEL { get; set; }
        public bool 已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}