namespace Exchange.UI.Models
{
    public class ConvertRequest
    {        
        public string FromISO { get; set; }        
        public string ToISO { get; set; }        
        public string Provider { get; set; }        
        public decimal Amount { get; set; }
    }
}
