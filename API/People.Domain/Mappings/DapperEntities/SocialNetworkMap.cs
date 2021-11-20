using Dapper.FluentMap.Dommel.Mapping;
using People.Domain.Entities;

namespace People.Domain.Mappings.Entities
{
    public class SocialNetworkMap : DommelEntityMap<SocialNetworkEntity>
    {
        public SocialNetworkMap()
        {
            ToTable("SocialNetwork");
            Map(x => x.Id).IsKey();
        }
    }
}
