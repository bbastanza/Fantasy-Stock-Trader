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
            
            References(x => x.User).Column("user_id");
            // References(x => x.Holding).Column("holding_id");
            
            Map(x => x.Amount).Column("amount");
            Map(x => x.SellAll).Column("sell_all");
            Map(x => x.TransactionPrice).Column("transaction_price");
            Map(x => x.TransactionDate).Column("transaction_date");
            Table("transaction_table");

        }
    }
}