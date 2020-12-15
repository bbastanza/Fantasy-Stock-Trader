using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class UserSessionMap : ClassMap<UserSession>
    {
        public UserSessionMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Map(x => x.InitDateTime).Column("init");
            Map(x => x.ExpireDateTime).Column("expire");
            Map(x => x.GuidString).Column("guid");
            References(x => x.User).Column("user_id");
            Table("session_table");
        }
    }

}