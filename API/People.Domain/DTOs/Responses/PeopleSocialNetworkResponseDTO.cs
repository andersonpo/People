namespace People.Domain.DTOs.Requests
{
    public class PeopleSocialNetworkResponseDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string SocialNetworkId { get; set; }
    }
}
