using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("Peoples")]
    public class PeopleEntity : BaseEntity<string>
    {
        public PeopleEntity()
        {
            SocialNetworks = new List<SocialNetworkEntity>();
            Clients = new List<ClientEntity>();
            TechnicalKnowledge = new List<TechnicalRatingEntity>();
        }

        [EntityColumn("FullName", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string FullName { get; set; }
        [EntityColumn("Birthdate", false, EntityColumn.DataType.DATE, 0, false)]
        public DateOnly Birthdate { get; set; }
        [EntityColumn("EmailCorporate", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string EmailCorporate { get; set; }
        [EntityColumn("EmailPersonal", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string EmailPersonal { get; set; }
        [EntityColumn("TelephoneCorporate", false, EntityColumn.DataType.VARCHAR, 20, false)]
        public string TelephoneCorporate { get; set; }
        [EntityColumn("TelephonePersonal", false, EntityColumn.DataType.VARCHAR, 20, false)]
        public string TelephonePersonal { get; set; }
        public IList<SocialNetworkEntity> SocialNetworks { get; set; }
        public IList<ClientEntity> Clients { get; set; }
        public IList<TechnicalRatingEntity> TechnicalKnowledge { get; set; }
    }
}
