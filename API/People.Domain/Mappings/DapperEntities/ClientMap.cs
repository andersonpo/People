using Dapper.FluentMap.Dommel.Mapping;
using People.Domain.Entities;

namespace People.Domain.Mappings.Entities
{
    public class ClientMap : DommelEntityMap<ClientEntity>
    {
        public ClientMap()
        {
            ToTable("Client");
            Map(x => x.Id).IsKey();
        }
    }
}
