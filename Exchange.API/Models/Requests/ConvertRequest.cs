using System.ComponentModel.DataAnnotations;

namespace Exchange.API.Models.Requests
{
    public class ConvertRequest
    {
        [Required]
        public string FromISO { get; set; }
        [Required]
        public string ToISO { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public decimal Amount { get; set; }        
    }
}
