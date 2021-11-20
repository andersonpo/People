using Dapper.FluentMap.Dommel.Mapping;
using People.Domain.Entities;

namespace People.Domain.Mappings.Entities
{
    public class TechnologyMap : DommelEntityMap<TechnologyEntity>
    {
        public TechnologyMap()
        {
            ToTable("Technology");
            Map(x => x.Id).IsKey();
        }
    }
}
