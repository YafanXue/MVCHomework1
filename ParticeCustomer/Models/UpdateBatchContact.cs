using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ParticeCustomer.Models
{
    public class UpdateBatchContact:IValidatableObject
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]

        public string 手機 { get; set; }
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        public int 客戶Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();
            const string regexPattern = @"\d{4}-\d{6}";
            var regex = new Regex(regexPattern);
            if (this.手機 != null)
            {
                if (!regex.IsMatch(this.手機))
                    yield return new ValidationResult("手機格式錯誤,電話格式必須為( e.g. 0911-111111 )", new[] { "Mobile" });
            }
        }
    }

    
}