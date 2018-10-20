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

        [HttpGet]
        public CoinPriceUpdate GetCoinPrice()
        {
            return _coinPriceUpdateCollection.Find(it=>true).SortByDescending(it=>it.At).FirstOrDefault();
        }

        [HttpGet("{id}")]
        public CoinPrice GetCoinPrice(string id)
        {
            var lastUpdateCoin = GetCoinPrice();
            return lastUpdateCoin.PriceList.FirstOrDefault(it=>it.Symbol.Equals(id,StringComparison.CurrentCultureIgnoreCase));
        }

        [HttpPost]
        public void BuyCoin(string username, string symbol, double coinamount){
            var coin = GetCoinPrice(symbol);

            var customer = _customerCollection.Find(it=>it.Username.Equals(username,StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            customer.Coins.Add(new CustomerCoin{
                Symbol = coin.Symbol,
                BuyingAt = DateTime.UtcNow,
                BuyingRate = coin.Buy,
                USDValue  = coinamount * coin.Buy,
            });
            _customerCollection.ReplaceOne(it=>it.Username.Equals(username,StringComparison.CurrentCultureIgnoreCase), customer);
        }

        [HttpPost]
        public void UpdateCoin([FromBody]CoinPriceUpdate request){
            _coinPriceUpdateCollection.InsertOne(request);
        }

        [HttpGet]
        public IEnumerable<CustomerWallet> GetCustomers(){
            return _customerCollection.Find(it=>true).ToList();
        }
        
        [HttpGet("{id}")]
        public CustomerWallet GetCustomer(string id){
            return _customerCollection.Find(it=>it.Username.Equals(id,StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        [HttpPost("{id}")]
        public void RegisterCustomer(string id){
            _customerCollection.InsertOne(new CustomerWallet{
                Username = id,
                Coins = new List<CustomerCoin>(),
            });
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "jdoe", "ptparker" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> Get(string id)
        {
            return new CustomerWallet
            {
                Username = "jdoe",
                Coins = new List<CustomerCoin>
                {
                    new CustomerCoin
                    {
                        Symbol = "BTC",
                        BuyingRate = 6565.25,
                        BuyingAt = new DateTime(2018, 10, 9, 9, 32, 23),
                        USDValue = 6500
                    },
                    new CustomerCoin
                    {
                        Symbol = "ETH",
                        BuyingRate = 203.47,
                        BuyingAt = new DateTime(2018, 9, 7, 12, 38, 33),
                        USDValue = 200.23
                    },
                },
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
        {
        }
    }
}
