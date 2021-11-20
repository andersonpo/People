namespace People.Domain.Configurations
{
    public class DataBaseConfigOptions
    {
        public const string NameConfig = "DataBaseConfig";
        public string? SqlServerConnectionString { get; set; }
    }
}
