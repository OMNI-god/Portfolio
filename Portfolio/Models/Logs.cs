using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Mode { get; set; }
        public string Mode_Id { get; set; }
        public double Amount { get; set; }
    }
}
