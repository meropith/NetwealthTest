using System;
using System.Collections.Generic;

namespace Exchage.Function.Models.DB
{
    public partial class Currency
    {
        public Currency()
        {
            ExchangeRates = new HashSet<ExchangeRate>();
            FixerRates = new HashSet<FixerRate>();
        }

        public int Id { get; set; }
        public string Isocode { get; set; }
        public string Name { get; set; }
        public bool IsSupported { get; set; }

        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
        public virtual ICollection<FixerRate> FixerRates { get; set; }
    }
}
