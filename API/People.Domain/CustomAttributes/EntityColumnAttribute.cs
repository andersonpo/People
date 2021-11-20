namespace People.Domain.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class EntityColumn : Attribute
    {
        public enum DataType
        {
            VARCHAR = 0,
            CHAR,
            BIT,
            SMALLINT,
            INT,            
            BIGINT,            
            FLOAT,
            REAL,
            DATETIME,
            DATE,
            TIME,
            TEXT,
            BINARY
        }

        private string columnName;
        private bool primaryKey;
        private DataType columnType;
        private int? columnSize;
        private bool acceptNull;

        public EntityColumn(string columnName, bool primaryKey, DataType columnType, int columnSize = 0, bool acceptNull = true)
        {
            this.columnName = columnName;
            this.primaryKey = primaryKey;
            this.columnType = columnType;
            this.columnSize = columnSize;
            this.acceptNull = acceptNull;
        }

        public string ColumnName
        {
            get { return columnName; }
        }

        public bool PrimaryKey
        {
            get { return primaryKey; }
        }

        public DataType ColumnType
        {
            get { return columnType; }
        }


        public int? ColumnSize
        {
            get { return columnSize; }
        }

        public bool AcceptNull
        {
            get { return acceptNull; }
        }

        public string ColumnTypeAndSize()
        {
            if (columnSize > 0)
                return $"{ColumnType}({columnSize})";
            else
                return ColumnType.ToString();
        }
    }
}
