using System;
using System.Collections.Generic;

namespace Exchange.API.Models.DB
{
    public partial class ExchangerRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal ToUsdrate { get; set; }

        public virtual Currency Currency { get; set; } = null!;
    }
}
