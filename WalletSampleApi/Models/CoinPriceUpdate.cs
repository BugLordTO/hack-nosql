using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class CoinPriceUpdate
    {
        public string Id { get; set; }
        public DateTime At { get; set; }
        public List<CoinPrice> PriceList { get; set; }
    }

    public class CoinPrice
    {
        public string Symbol { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
    }
}
