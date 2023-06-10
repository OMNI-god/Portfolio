using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Bank_Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string ROI { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Investment_Start_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Maturity_Date { get; set; }
        [Required]
        public double Investment_Amount { get; set; }
        public double Maturity_Amount { get; set; }
        public string Uemail { get; set; }
        
    }
}
