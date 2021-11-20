namespace People.Domain.DTOs.Requests
{
    public class PeopleClientRequestDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string ClientId { get; set; }
    }
}
