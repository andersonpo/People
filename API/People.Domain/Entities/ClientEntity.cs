using People.Domain.CustomAttributes;

namespace People.Domain.Entities
{
    [EntityTable("Clients")]
    public class ClientEntity : BaseEntity<string>
    {
        [EntityColumn("Name", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string Name { get; set; }
        [EntityColumn("Logo", false, EntityColumn.DataType.TEXT, 0, true)]
        public string Logo { get; set; }
        [EntityColumn("Link", false, EntityColumn.DataType.VARCHAR, 200, false)]
        public string Link { get; set; }
        [EntityColumn("DateStart", false, EntityColumn.DataType.DATE, 0, false)]
        public DateOnly DateStart { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [EntityColumn("DateEnd", false, EntityColumn.DataType.DATE, 0, true)]
        public DateOnly? DateEnd { get; set; }
    }
}
