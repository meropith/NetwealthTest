namespace Exchange.API.Models.Responces
{
    public class FixerConvertResponce
    {
        public string Date { get; set; } = String.Empty;
        public string Historical { get; set; } = String.Empty;
        public Info Info { get; set; } = new Info();
        public Query Query { get; set; } = new Query();
        public decimal Result { get; set; }
        public bool Success { get; set; }
    }

    public class Info
    {
        public decimal Rate { get; set; }
        public int Timestamp { get; set; }
    }

    public class Query
    {
        public decimal Amount { get; set; }
        public string From { get; set; } = String.Empty;
        public string To { get; set; } = String.Empty;
    }

    


}
