using System;
using Core.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class TransactionModel
    {
        public TransactionModel(Transaction transaction)
        {
            Type = transaction.Type;
            Date = transaction.TransactionDate;
            Amount = transaction.Amount;
            TransactionPrice = transaction.TransactionPrice;
            Symbol = transaction.Holding.Symbol;
            CompanyName = transaction.Holding.CompanyName;
        }
        
        public string Symbol { get; set;  }
        public string CompanyName { get;set;  }
        public double TransactionPrice { get;set;  }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}