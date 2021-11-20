using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("PeopleClients")]
    public class PeopleClientEntity
    {
        [EntityColumn("PeopleId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string PeopleId { get; set; }
        [EntityColumn("ClientId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string ClientId { get; set; }

        public PeopleEntity People { get; set; }
        public ClientEntity Client { get; set; }
    }
}
