namespace People.Domain.DTOs.Requests
{
    public class PeopleClientResponseDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string ClientId { get; set; }
    }
}
