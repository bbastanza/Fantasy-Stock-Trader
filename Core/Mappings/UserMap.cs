using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Map(x => x.UserName).Column("user-name");
            Map(x => x.Password).Column("password");
            Map(x => x.Email).Column("email");
            Map(x => x.CreatedAt).Column("created-at");
            Map(x => x.Balance).Column("balance");
            // HasMany(x => x.Holdings)
            //     .Cascade.All()
            //     .Table("Holdings");
            Table("user_table");
        }
    }
}