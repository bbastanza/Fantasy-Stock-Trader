using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Map(x => x.Type).Column("type");
            Component(x => x.User,
                userId => userId
                    .Map(x => x.Id).Column("user_id"));
            Component(x => x.Holding,
                holdingId => holdingId
                        .Map(x => x.Id).Column("holding_id"));
            Map(x => x.Amount).Column("amount");
            Map(x => x.TransactionPrice).Column("transaction_price");
            Map(x => x.TransactionDate).Column("transaction_date");
            Table("transaction_table");
        }
    }
}