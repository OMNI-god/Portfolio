using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class User
    {
        [Required]
        public string FullName { get; set; }

        [Key]
        [Required,DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Minimum length 8 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public double? Salary { get;set; }
        [DataType(DataType.Date)]
        public DateTime? DOJ {  get; set; }  
        public double? Banking_return { get; set; }
        public double? Stock_return { get; set; }
        public double? SIP_return { get; set; }
        public double? Miscellaneous_return { get; set; }
        public double? Total_savings { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Last_update_date { get; set; }
    }
}
