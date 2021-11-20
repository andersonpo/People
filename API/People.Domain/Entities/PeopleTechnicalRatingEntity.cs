using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("PeopleTechnicalRatings")]
    public class PeopleTechnicalRatingEntity
    {
        [EntityColumn("PeopleId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string PeopleId { get; set; }
        [EntityColumn("TechnicalRatingId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string TechnicalRatingId { get; set; }

        public PeopleEntity People { get; set; }
        public TechnicalRatingEntity TechnicalRating { get; set; }
    }
}
