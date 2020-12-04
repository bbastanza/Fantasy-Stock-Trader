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
            
        public virtual string Symbol { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual double Value { get; set; }
        public virtual double TotalShares { get; set; }
        
    }
}