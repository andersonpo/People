namespace People.Domain.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class EntityTable : Attribute
    {
        private string tableName;

        public EntityTable(string tableName)
        {
            this.tableName = tableName;
        }

        public string TableName
        {
            get { return tableName; }
        }
    }
}
