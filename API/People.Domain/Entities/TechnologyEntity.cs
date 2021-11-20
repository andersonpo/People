using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("Technologies")]
    public class TechnologyEntity : BaseEntity<string>
    {
        [EntityColumn("Name", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string Name { get; set; }
        [EntityColumn("Logo", false, EntityColumn.DataType.TEXT, 0, false)]
        public string Logo { get; set; }
        [EntityColumn("Link", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string Link { get; set; }

    }
}
