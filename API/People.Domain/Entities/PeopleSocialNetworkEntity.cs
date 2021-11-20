using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("PeopleSocialNetworks")]
    public class PeopleSocialNetworkEntity
    {
        [EntityColumn("PeopleId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string PeopleId { get; set; }
        [EntityColumn("SocialNetworkId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string SocialNetworkId { get; set; }

        public PeopleEntity People { get; set; }
        public SocialNetworkEntity SocialNetwork { get; set; }
    }
}
