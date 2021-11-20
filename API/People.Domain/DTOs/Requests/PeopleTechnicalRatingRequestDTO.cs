namespace People.Domain.DTOs.Requests
{
    public class PeopleTechnicalRatingRequestDTO : BaseRequestDTO<string>
    {
        public string PeopleId { get; set; }
        public string TechnicalRatingId { get; set; }
    }
}
