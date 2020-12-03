using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class HoldingMap : ClassMap<Holding>
    {

        public HoldingMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Component(x => x.User,
                userId => userId.Map(x => x.Id).Column("user_id"));
            Map(x => x.Symbol).Column("symbol");
            Map(x => x.TotalShares).Column("total_shares");
            Table("holding_table");
            // References(x => x.User);
        }
    }
}