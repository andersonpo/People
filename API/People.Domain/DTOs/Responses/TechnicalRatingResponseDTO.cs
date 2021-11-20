namespace People.Domain.DTOs.Requests
{
    public class TechnicalRatingResponseDTO : BaseRequestDTO<string>
    {
        public string TechnologyId { get; set; }
        public int Rating { get; set; }
        public DateOnly RatingDate { get; set; }
    }
}
