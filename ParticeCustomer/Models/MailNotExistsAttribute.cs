using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace ParticeCustomer.Models
{
    public class MailNotExistsAttribute : ValidationAttribute
    {
        public MailNotExistsAttribute()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string str = (string)value;
            var owner = validationContext.ObjectInstance as 客戶聯絡人;
            if (owner == null) return new ValidationResult("Model is empty");
            客戶資料Entities db = new 客戶資料Entities();
            
            var data = db.客戶聯絡人.FirstOrDefault(p => p.Email == str && p.客戶Id==owner.客戶Id && p.Id != owner.Id);
            if (data != null)
                return new ValidationResult("Email帳號重複");
            else
                return ValidationResult.Success;
        }
    }
}