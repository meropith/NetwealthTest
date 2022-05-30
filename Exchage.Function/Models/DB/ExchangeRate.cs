using System;
using System.Collections.Generic;

namespace Exchage.Function.Models.DB
{
    public partial class ExchangeRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal ToUsdrate { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
