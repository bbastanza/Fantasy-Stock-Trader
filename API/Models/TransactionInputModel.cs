namespace API.Models
{
    public class TransactionInputModel
    {
        public string Symbol { get; set; }
        public double PurchaseAmount { get; set; }
        public string Type { get; set; }
        public bool SellAll { get; set; }
        public string UserName { get; set; }
    }
}