using Core.Entities;
using Core.Entities.Users;
using FluentNHibernate.Mapping;

namespace Core.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            // Id(x => x.Id);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.Email);
            Map(x => x.Email);
            Map(x => x.CreatedAt);
            Map(x => x.Balance);
            HasMany(x => x.Holdings)
                .Cascade.All()
                .Table("Holdings");
        }
    }
}