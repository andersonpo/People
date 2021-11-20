namespace People.Domain.DTOs.Requests
{
    public class TechnologyRequestWithoutIdDTO : BaseRequestWithoutIdDTO
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Link { get; set; }
    }
}
