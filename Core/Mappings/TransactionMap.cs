using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Type).Column("type");
            Component(x => x.User,
                userId => userId.Map(x => x.Id, "userId"));
            Component(x => x.Holding,
                holdingId => holdingId.Map(x => x.Id, "holdingId"));
            Map(x => x.Amount).Column("amount");
            Map(x => x.TransactionPrice).Column("transactionPrice");
            Map(x => x.TransactionDate).Column("transactionDate");
            Table("transaction_table");
        }
    }
}