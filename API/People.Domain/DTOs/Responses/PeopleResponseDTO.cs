namespace People.Domain.DTOs.Requests
{
    public class PeopleResponseDTO : BaseRequestDTO<string>
    {
        public PeopleResponseDTO()
        {
            this.SocialNetworks = new List<SocialNetworkRequestDTO>();
            this.Clients = new List<ClientRequestDTO>();
            this.TechnicalKnowledge = new List<TechnicalRatingRequestDTO>();
        }

        public string FullName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string EmailCorporate { get; set; }
        public string EmailPersonal { get; set; }
        public string TelephoneCorporate { get; set; }
        public string TelephonePersonal { get; set; }
        public IList<SocialNetworkRequestDTO> SocialNetworks { get; set; }
        public IList<ClientRequestDTO> Clients { get; set; }
        public IList<TechnicalRatingRequestDTO> TechnicalKnowledge { get; set; }
    }
}
