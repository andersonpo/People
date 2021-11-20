namespace People.Domain.DTOs.Requests
{
    public class BaseRequestDTO<K> where K : IComparable, IConvertible, IEquatable<K>
    {
        public K Id { get; set; }
    }
}
