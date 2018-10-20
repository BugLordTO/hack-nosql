using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletSampleApi.Models;
using MongoDB.Driver;

namespace WalletSampleApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HackController : ControllerBase
    {
        private IMongoDatabase _database;
        private IMongoCollection<CustomerWallet> _customerCollection;
        private IMongoCollection<CoinPriceUpdate> _coinPriceUpdateCollection;

        public HackController()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://admin:admin1234@ds237373.mlab.com:37373/hackcoin"));
            var mongoClient = new MongoClient(settings);
            _database = mongoClient.GetDatabase("hackcoin");
            _customerCollection = _database.GetCollection<CustomerWallet>("CustomerWallets");
            _coinPriceUpdateCollection = _database.GetCollection<CoinPriceUpdate>("CoinPriceUpdates");
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var customers = _customerCollection.Find(it => true).ToList();
            return customers.Select(it => it.Username).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            var customer = _customerCollection.Find(it => it.Username.Equals(id, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            customer.TotalCoins = customer.Coins.GroupBy(it => it.Symbol).Select(it => new TotalCoin
            {
                Symbol = it.Key,
                Amount = it.Sum(tc => tc.USDValue / tc.BuyingRate),
            }).ToList();
            return customer;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
        {
            _coinPriceUpdateCollection.InsertOne(updateCoin);
        }

        [HttpGet]
        public CoinPriceUpdate GetCoinPrice()
        {
            return _coinPriceUpdateCollection.Find(it => true).SortByDescending(it => it.At).FirstOrDefault();
        }

        [HttpGet("{id}")]
        public CoinPrice GetCoinPrice(string id)
        {
            var lastUpdateCoin = GetCoinPrice();
            return lastUpdateCoin?.PriceList?.FirstOrDefault(it => it.Symbol.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        }

        [HttpPost("{username}/{symbol}/{coinamount}")]
        public void BuyCoin(string username, string symbol, double coinamount)
        {
            var coin = GetCoinPrice(symbol);
            if (coin == null) return;

            var customer = _customerCollection.Find(it => it.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (customer == null) return;

            var customerCoin = new CustomerCoin
            {
                Symbol = coin.Symbol,
                BuyingAt = DateTime.UtcNow,
                BuyingRate = coin.Buy,
                USDValue = coinamount * coin.Buy,

            };
            customer.Coins.Add(customerCoin);

            _customerCollection.ReplaceOne(it => it.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase), customer);
        }

        [HttpPost("{username}/{symbol}/{coinamount}")]
        public void SellCoin(string username, string symbol, double coinamount)
        {
            var coin = GetCoinPrice(symbol);
            if (coin == null) return;

            var customer = _customerCollection.Find(it => it.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (customer == null) return;

            var customerCoin = new CustomerCoinSell
            {
                Symbol = coin.Symbol,
                SellingAt = DateTime.UtcNow,
                SellingRate = coin.Sell,
                USDValue = coinamount * coin.Sell,
            };
            customer.CoinSells.Add(customerCoin);

            _customerCollection.ReplaceOne(it => it.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase), customer);
        }

        [HttpPost("{id}")]
        public void RegisterCustomer(string id)
        {
            _customerCollection.InsertOne(new CustomerWallet
            {
                Username = id,
                Coins = new List<CustomerCoin>(),
                CoinSells = new List<CustomerCoinSell>(),
            });
        }
    }
}
