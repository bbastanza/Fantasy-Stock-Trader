using API.Models;
using Core.Entities.Transactions;
using Core.Models;

namespace API.Mappings
{
    public interface ITransactionMapper
    {
        Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData);
    }
    public class TransactionMapper : ITransactionMapper
    {

        public Transaction MapTransaction(TransactionInputModel transactionInput, IexStockModel iexData)
        {
            var transaction = new Transaction();
            transaction.Amount = transactionInput.PurchaseAmount;
            transaction.Symbol = iexData.Symbol;
            transaction.CompanyName = iexData.CompanyName;
            transaction.User.UserName = transactionInput.UserName;
            transaction.CurrentPrice = iexData.LatestPrice;
            transaction.SellAll = transactionInput.SellAll;
            return transaction;
        }
    }
}