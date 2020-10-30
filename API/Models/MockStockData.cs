using System.Text.Json.Serialization;

namespace API.Models
{
    public class MockStockData
    {
        [JsonPropertyName("stock_name")]
        public string StockName { get; set; }
        [JsonPropertyName("user_amount")]
        public float UserAmount { get; set; }

        public MockStockData(string stockName, float userAmount)
        {
            StockName = stockName;
            UserAmount = userAmount;
        }
    }
}