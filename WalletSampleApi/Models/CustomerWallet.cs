using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSampleApi.Models
{
    public class CustomerWallet
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<CustomerCoin> Coins { get; set; }
        public List<CustomerCoinSell> CoinSells { get; set; }
        public List<TotalCoin> TotalCoins { get; set; }
    }

    public class CustomerCoin
    {
        public string Symbol { get; set; }
        /// <summary>
        /// ราคาตอนซื้อ
        /// </summary>
        public double BuyingRate { get; set; }
        /// <summary>
        /// ซื้อเมื่อไหร่
        /// </summary>
        public DateTime BuyingAt { get; set; }
        /// <summary>
        /// มูลค่าหากแลกเป็น USD ตอนนี้
        /// </summary>
        public double USDValue { get; set; }
    }

    public class CustomerCoinSell
    {
        public string Symbol { get; set; }
        /// <summary>
        /// ราคาตอนซื้อ
        /// </summary>
        public double SellingRate { get; set; }
        /// <summary>
        /// ซื้อเมื่อไหร่
        /// </summary>
        public DateTime SellingAt { get; set; }
        /// <summary>
        /// มูลค่าหากแลกเป็น USD ตอนนี้
        /// </summary>
        public double USDValue { get; set; }
    }

    public class TotalCoin
    {
        public string Symbol { get; set; }
        public double Amount { get; set; }
    }
}
