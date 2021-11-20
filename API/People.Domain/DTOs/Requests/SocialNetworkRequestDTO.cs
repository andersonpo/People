namespace People.Domain.DTOs.Requests
{
    public class SocialNetworkRequestDTO : BaseRequestDTO<string>
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }
}
