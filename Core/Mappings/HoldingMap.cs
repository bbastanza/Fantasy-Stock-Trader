using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class HoldingMap : ClassMap<Holding>
    {

        public HoldingMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Map(x => x.Symbol).Column("symbol");
            Map(x => x.CompanyName).Column("company_name");
            Map(x => x.TotalShares).Column("total_shares");
            References(x => x.User).Column("user_id");
            Table("holding_table");
        }
    }
}