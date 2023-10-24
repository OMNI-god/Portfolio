using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Event_Resources
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Bank_Name { get; set; }
        [Required]
        public string Type { get; set; }
        [DataType(DataType.Date)]
        public DateTime Maturity_Date { get; set; }
        public double Maturity_Amount { get; set; }
        public string Time_Left_To_Mature { get; set; }
        public int Event_ID { get; set; }  
    }
}
