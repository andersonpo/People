namespace People.Domain.DTOs.Requests
{
    public class BaseResponseDTO<K> where K : IComparable, IConvertible, IEquatable<K>
    {
        public K Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
        public string CreatedUserId { get; set; } = string.Empty;
        public string? UpdatedUserId { get; set; }
    }
}
