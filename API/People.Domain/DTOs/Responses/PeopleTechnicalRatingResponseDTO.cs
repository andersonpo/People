namespace People.Domain.DTOs.Requests
{
    public class PeopleTechnicalRatingResponseDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string TechnicalRatingId { get; set; }
    }
}
