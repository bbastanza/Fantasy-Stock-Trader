using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("Id");
            Map(x => x.UserName).Column("userName");
            Map(x => x.Password).Column("password");
            Map(x => x.Email).Column("email");
            Map(x => x.CreatedAt).Column("createdAt");
            Map(x => x.Balance).Column("balance");
            // HasMany(x => x.Holdings)
            //     .Cascade.All()
            //     .Table("Holdings");
            Table("user_table");
        }
    }
}