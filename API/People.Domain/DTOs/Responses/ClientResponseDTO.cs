namespace People.Domain.DTOs.Requests
{
    public class ClientResponseDTO : BaseRequestDTO<string>
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Link { get; set; }
        public DateOnly DateStart { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DateEnd { get; set; }
    }
}
