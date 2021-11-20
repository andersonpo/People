using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("SocialNetworks")]
    public class SocialNetworkEntity : BaseEntity<string>
    {
        [EntityColumn("Name", false, EntityColumn.DataType.VARCHAR, 100, false)]
        public string Name { get; set; }
        [EntityColumn("Link", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string Link { get; set; }
        [EntityColumn("Icon", false, EntityColumn.DataType.TEXT, 0, false)]
        public string Icon { get; set; }

    }
}
