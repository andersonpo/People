using People.Domain.CustomAttributes;
using People.Domain.Interfaces;

namespace People.Domain.Entities
{
    public class BaseEntity<K> : IBaseEntity
        where K : IComparable, IConvertible, IEquatable<K>
    {
        [EntityColumn("Id", true, EntityColumn.DataType.VARCHAR, 60, false)]
        public K Id { get; set; }
        [EntityColumn("DateCreated", false, EntityColumn.DataType.DATETIME, 0, false)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [EntityColumn("DateUpdated", false, EntityColumn.DataType.DATETIME, 0, false)]
        public DateTime? DateUpdated { get; set; }
        [EntityColumn("CreatedUserId", false, EntityColumn.DataType.VARCHAR, 60, false)]
        public string CreatedUserId { get; set; } = string.Empty;
        [EntityColumn("UpdatedUserId", false, EntityColumn.DataType.VARCHAR, 60, true)]
        public string? UpdatedUserId { get; set; }
    }
}
