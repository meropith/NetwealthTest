namespace Exchange.API.Models.Responces
{

    public class ExchangeRatesResponce
    {
        public string Date { get; set; } = String.Empty;
        public string Historical { get; set; } = String.Empty;
        public Info Info { get; set; } = new Info();
        public Query Query { get; set; } = new Query();
        public decimal Result { get; set; }
        public bool Success { get; set; }
    }   

}
