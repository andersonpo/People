using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("TechnicalRatings")]
    public class TechnicalRatingEntity : BaseEntity<string>
    {
        [EntityColumn("TechnologyId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string TechnologyId { get; set; }
        public TechnologyEntity Technology { get; set; }
        [EntityColumn("Rating", false, EntityColumn.DataType.SMALLINT, 0, false)]
        public int Rating { get; set; }
        [EntityColumn("RatingDate", false, EntityColumn.DataType.DATETIME, 0, false)]
        public DateOnly RatingDate { get; set; }
    }
}
