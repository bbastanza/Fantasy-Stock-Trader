namespace Core.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class StockModel    {
        public string Symbol { get; set; } 
        public string CompanyName { get; set; } 
        public string PrimaryExchange { get; set; } 
        public string CalculationPrice { get; set; } 
        public int Open { get; set; } 
        public long OpenTime { get; set; } 
        public string OpenSource { get; set; } 
        public double Close { get; set; } 
        public long CloseTime { get; set; } 
        public string CloseSource { get; set; } 
        public double High { get; set; } 
        public long HighTime { get; set; } 
        public string HighSource { get; set; } 
        public double Low { get; set; } 
        public long LowTime { get; set; } 
        public string LowSource { get; set; } 
        public double LatestPrice { get; set; } 
        public string LatestSource { get; set; } 
        public string LatestTime { get; set; } 
        public long LatestUpdate { get; set; } 
        public int LatestVolume { get; set; } 
        public double IexRealtimePrice { get; set; } 
        public int IexRealtimeSize { get; set; } 
        public long IexLastUpdated { get; set; } 
        public double DelayedPrice { get; set; } 
        public long DelayedPriceTime { get; set; } 
        public double OddLotDelayedPrice { get; set; } 
        public long OddLotDelayedPriceTime { get; set; } 
        public int ExtendedPrice { get; set; } 
        public double ExtendedChange { get; set; } 
        public double ExtendedChangePercent { get; set; } 
        public long ExtendedPriceTime { get; set; } 
        public double PreviousClose { get; set; } 
        public int PreviousVolume { get; set; } 
        public double Change { get; set; } 
        public double ChangePercent { get; set; } 
        public int Volume { get; set; } 
        public double IexMarketPercent { get; set; } 
        public int IexVolume { get; set; } 
        public int AvgTotalVolume { get; set; } 
        public int IexBidPrice { get; set; } 
        public int IexBidSize { get; set; } 
        public int IexAskPrice { get; set; } 
        public int IexAskSize { get; set; } 
        public object IexOpen { get; set; } 
        public object IexOpenTime { get; set; } 
        public double IexClose { get; set; } 
        public long IexCloseTime { get; set; } 
        public long MarketCap { get; set; } 
        public double PeRatio { get; set; } 
        public double Week52High { get; set; } 
        public double Week52Low { get; set; } 
        public double YtdChange { get; set; } 
        public long LastTradeTime { get; set; } 
        public bool IsUsMarketOpen { get; set; } 
    }
}