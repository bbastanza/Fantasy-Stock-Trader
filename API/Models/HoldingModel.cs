using System.Text.Json.Serialization;
using Core.Entities;

namespace API.Models
{
    public class HoldingModel
    {
        public HoldingModel(Holding holding)
        {
            Symbol = holding.Symbol;
            CompanyName = holding.CompanyName;
            Value = holding.Value;
            TotalShares = holding.TotalShares;
        }
            
        [JsonPropertyName("symbol")]
        public virtual string Symbol { get; set; }
        
        [JsonPropertyName("companyName")]
        public virtual string CompanyName { get; set; }
        
        [JsonPropertyName("value")]
        public virtual double Value { get; set; }
        
        [JsonPropertyName("totalShares")]
        public virtual double TotalShares { get; set; }
        
    }
}