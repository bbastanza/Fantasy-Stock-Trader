using System;
using Core.Entities;

namespace API.Models
{
    public class TransactionModel
    {
        public TransactionModel(Transaction transaction, Holding holding)
        {
            Type = transaction.Type;
            Date = transaction.TransactionDate;
            Amount = transaction.Amount;
            TransactionPrice = transaction.TransactionPrice;
            Symbol = holding.Symbol;
            CompanyName = holding.CompanyName;
        }
        
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public double TransactionPrice { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
    }
}