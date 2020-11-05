using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class StockModel    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("primaryExchange")]
        public string PrimaryExchange { get; set; }
        [JsonPropertyName("calculationPrice")]
        public string CalculationPrice { get; set; }
        [JsonPropertyName("open")]
        public int Open { get; set; }
        [JsonPropertyName("openTime")]
        public long OpenTime { get; set; }
        [JsonPropertyName("openSource")]
        public string OpenSource { get; set; }
        [JsonPropertyName("close")]
        public double Close { get; set; }
        [JsonPropertyName("closeTime")]
        public long CloseTime { get; set; }
        [JsonPropertyName("closeSource")]
        public string CloseSource { get; set; }
        [JsonPropertyName("symbol")]
        public double High { get; set; }
        [JsonPropertyName("high")]
        public long HighTime { get; set; }
        [JsonPropertyName("highSource")]
        public string HighSource { get; set; }
        [JsonPropertyName("low")]
        public double Low { get; set; }
        [JsonPropertyName("lowTime")]
        public long LowTime { get; set; }
        [JsonPropertyName("lowSource")]
        public string LowSource { get; set; }
        [JsonPropertyName("latestPrice")]
        public double LatestPrice { get; set; }
        [JsonPropertyName("latestSource")]
        public string LatestSource { get; set; }
        [JsonPropertyName("latestTime")]
        public string LatestTime { get; set; }
        [JsonPropertyName("latestUpdate")]
        public long LatestUpdate { get; set; }
        [JsonPropertyName("latestVolume")]
        public int LatestVolume { get; set; }
        [JsonPropertyName("iexRealtimePrice")]
        public double IexRealtimePrice { get; set; }
        [JsonPropertyName("iexRealtimeSize")]
        public int IexRealtimeSize { get; set; }
        [JsonPropertyName("iexLastUpdated")]
        public long IexLastUpdated { get; set; }
        [JsonPropertyName("delayedPrice")]
        public double DelayedPrice { get; set; }
        [JsonPropertyName("delayedPriceTime")]
        public long DelayedPriceTime { get; set; }
        [JsonPropertyName("oddLotDelayedPrice")]
        public double OddLotDelayedPrice { get; set; }
        [JsonPropertyName("oddLotDelayedPriceTime")]
        public long OddLotDelayedPriceTime { get; set; }
        [JsonPropertyName("extendedPrice")]
        public int ExtendedPrice { get; set; }
        [JsonPropertyName("extendedChange")]
        public double ExtendedChange { get; set; }
        [JsonPropertyName("extendedChangePrice")]
        public double ExtendedChangePercent { get; set; }
        [JsonPropertyName("extendedPriceTime")]
        public long ExtendedPriceTime { get; set; }
        [JsonPropertyName("previousClose")]
        public double PreviousClose { get; set; }
        [JsonPropertyName("previousVolume")]
        public int PreviousVolume { get; set; }
        [JsonPropertyName("change")]
        public double Change { get; set; }
        [JsonPropertyName("changePercent")]
        public double ChangePercent { get; set; }
        [JsonPropertyName("volume")]
        public int Volume { get; set; }
        [JsonPropertyName("iexMarketPercent")]
        public double IexMarketPercent { get; set; }
        [JsonPropertyName("iexVolume")]
        public int IexVolume { get; set; }
        [JsonPropertyName("avgTotalVolume")]
        public int AvgTotalVolume { get; set; }
        [JsonPropertyName("iexBidPrice")]
        public int IexBidPrice { get; set; }
        [JsonPropertyName("iexBidSize")]
        public int IexBidSize { get; set; }
        [JsonPropertyName("iexAskPrice")]
        public int IexAskPrice { get; set; }
        [JsonPropertyName("iexAskSize")]
        public int IexAskSize { get; set; }
        [JsonPropertyName("iexOpen")]
        public object IexOpen { get; set; }
        [JsonPropertyName("iexOpenTime")]
        public object IexOpenTime { get; set; }
        [JsonPropertyName("iexClose")]
        public double IexClose { get; set; }
        [JsonPropertyName("iexCloseTime")]
        public long IexCloseTime { get; set; }
        [JsonPropertyName("marketCap")]
        public long MarketCap { get; set; }
        [JsonPropertyName("peRatio")]
        public double PeRatio { get; set; }
        [JsonPropertyName("week52High")]
        public double Week52High { get; set; }
        [JsonPropertyName("week52Low")]
        public double Week52Low { get; set; }
        [JsonPropertyName("ytdChange")]
        public double YtdChange { get; set; }
        [JsonPropertyName("lastTradeTime")]
        public long LastTradeTime { get; set; }
        [JsonPropertyName("isUsMarketOpen")]
        public bool IsUsMarketOpen { get; set; }
    }
}