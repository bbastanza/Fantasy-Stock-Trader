using Core.Entities;
using Core.Entities.Transactions;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(x => x.Type);
            Map(x => x.Symbol);
            Map(x => x.CompanyName);
            Map(x => x.Amount);
            Map(x => x.CurrentPrice);
            Map(x => x.CreatedAt);
            Map(x => x.User);
        }
    }
}