using Dapper.FluentMap.Dommel.Mapping;
using People.Domain.Entities;

namespace People.Domain.Mappings.Entities
{
    public class TechnicalRatingMap : DommelEntityMap<TechnicalRatingEntity>
    {
        public TechnicalRatingMap()
        {
            ToTable("TechnicalRating");
            Map(x => x.Id).IsKey();
        }
    }
}
