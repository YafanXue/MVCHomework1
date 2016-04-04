namespace ParticeCustomer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();
            const string regexPattern = @"\d{4}-\d{6}";
            var regex = new Regex(regexPattern);
            if (this.手機 != null)
            {
                if (!regex.IsMatch(this.手機))
                    yield return new ValidationResult("手機格式錯誤,電話格式必須為( e.g. 0911-111111 )", new[] { "手機" });
            }
            客戶資料Entities db = new 客戶資料Entities();

            var data = db.客戶聯絡人.FirstOrDefault(p => p.Email == this.Email && p.客戶Id == this.客戶Id && p.Id != this.Id);
            if (data != null)
                yield return new ValidationResult("Email帳號重複", new[] { "Email" });
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        [EmailAddress(ErrorMessage = "Email格式錯誤")]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool 已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
