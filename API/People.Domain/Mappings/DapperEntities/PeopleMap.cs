using Dapper.FluentMap.Dommel.Mapping;
using People.Domain.Entities;

namespace People.Domain.Mappings.Entities
{
    public class PeopleMap : DommelEntityMap<PeopleEntity>
    {
        public PeopleMap()
        {
            ToTable("People");
            Map(x => x.Id).IsKey();
        }
    }
}
