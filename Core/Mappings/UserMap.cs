using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("id");
            Map(x => x.UserName).Column("username");
            Map(x => x.Password).Column("password");
            Map(x => x.Email).Column("email");
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.Balance).Column("balance");
            HasMany(x => x.Transactions)
                .KeyColumn("user_id")
                .Inverse()
                .Cascade.All();
            HasMany(x => x.Holdings)
                .KeyColumn("user_id")
                .Inverse()
                .Cascade.All();
            Table("user_table");
        }
    }
}