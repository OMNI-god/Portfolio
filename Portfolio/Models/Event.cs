using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Occasion_For { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Tentative_Required_Date { get; set; }
        public string User_Email { get; set; }
    }
}
