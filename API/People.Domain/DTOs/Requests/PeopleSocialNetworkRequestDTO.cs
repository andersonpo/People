namespace People.Domain.DTOs.Requests
{
    public class PeopleSocialNetworkRequestDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string SocialNetworkId { get; set; }
    }
}
