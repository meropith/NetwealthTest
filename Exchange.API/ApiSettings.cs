namespace Exchange.API
{
    public class ApiSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string FixerApiKey { get; set; } = string.Empty;
        public string FixerApiBaseURL { get; set; } = string.Empty;
        public string ExchangeRatesApiKey { get; set; } = string.Empty;
        public string ExchangeRatesBaseURL { get; set; } = string.Empty;
        public bool UseCache { get; set; }
    }
}
