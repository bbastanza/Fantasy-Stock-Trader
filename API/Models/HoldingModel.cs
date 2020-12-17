using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
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
            
        public virtual string Symbol { get; }
        public virtual string CompanyName { get; }
        public virtual double Value { get; }
        public virtual double TotalShares { get; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}