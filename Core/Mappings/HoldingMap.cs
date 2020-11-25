using Core.Entities.Users;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class HoldingMap : ClassMap<Holding>
    {

        public HoldingMap()
        {
            Map(x => x.Symbol);
            Map(x => x.CompanyName);
            Map(x => x.TotalShares);
            // References(x => x.User);
        }
    }
}