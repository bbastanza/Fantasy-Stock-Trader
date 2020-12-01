using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.Type);
            Component(x => x.User,
                userId => userId.Map(x => x.Id, "userId"));
            Component(x => x.Holding,
                holdingId => holdingId.Map(x => x.Id, "holdingId"));
            Map(x => x.Amount);
            Map(x => x.TransactionPrice);
            Map(x => x.TransactionDate);
            Table("transaction_table");
        }
    }
}