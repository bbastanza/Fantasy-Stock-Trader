using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class HoldingMap : ClassMap<Holding>
    {

        public HoldingMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Component(x => x.User,
                userId => userId.Map(x => x.Id, "userId"));
            Map(x => x.Symbol).Column("symbol");
            Map(x => x.TotalShares).Column("totalShares");
            Table("holding_table");
            // References(x => x.User);
        }
    }
}