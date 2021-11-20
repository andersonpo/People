using Microsoft.AspNetCore.Builder;
using People.Domain.CustomAttributes;
using People.Domain.Interfaces.Services;
using System.Data;
using System.Text;

namespace People.Infrastructure.Extensions.Database
{
    public static class DatabaseOperationExtensions
    {
        public static IApplicationBuilder DatabaseCreate(this IApplicationBuilder app, IDbConnection dbConnection, ILogService logService)
        {
            const string databaseName = "People";
            const string databaseDefault = "master";
            const string schemaName = "dbo";
            var success = CreateDatabaseFromDefault(dbConnection, databaseName, databaseDefault, logService);
            if (success)
            {
                CreateTables(dbConnection, schemaName, logService);
            }
            return app;
        }

        private static bool CreateDatabaseFromDefault(IDbConnection dbConnection, string databaseName, string databaseDefault, ILogService logService)
        {
            logService.LogDebug("[DatabaseOperationExtensions] Create Database");
            var backup = dbConnection.ConnectionString;
            try
            {                
                dbConnection.ConnectionString = dbConnection.ConnectionString.Replace($"={databaseName};", $"={databaseDefault};");
                dbConnection.Open();
                var command = dbConnection.CreateCommand();
                command.CommandText = $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}') BEGIN CREATE DATABASE {databaseName} END";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logService.LogException("[DatabaseOperationExtensions] Create Database", ex);
                return false;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.ConnectionString = backup;
            }
            return true;
        }

        private static void CreateTables(IDbConnection dbConnection, string schemaName, ILogService logService)
        {
            logService.LogDebug("[DatabaseOperationExtensions] Create Tables");
            var type = typeof(Domain.Interfaces.IBaseEntity);
            var entities = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => type.IsAssignableFrom(t)).ToList();
            entities.Remove(typeof(Domain.Interfaces.IBaseEntity));

            try
            {
                dbConnection.Open();
                entities.ForEach(t =>
                {
                    var columnsDefinition = new StringBuilder();
                    var pkColNames = string.Empty;

                    if (!t.Name.Contains("BaseEntity", StringComparison.OrdinalIgnoreCase))
                    {
                        var tableName = TableName(t);
                        var allProps = t.GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(EntityColumn))).ToList();

                        allProps.ForEach(p =>
                        {
                            var columnAttribute = ((EntityColumn[])p.GetCustomAttributes(typeof(EntityColumn), true)).FirstOrDefault<EntityColumn>();
                            if (columnAttribute != null)
                            {
                                var isLast = allProps.Last().Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase);
                                columnsDefinition.AppendLine($"\t{columnAttribute.ColumnName} {columnAttribute.ColumnTypeAndSize()} {(columnAttribute.AcceptNull ? "NULL" : "NOT NULL")}{(isLast ? string.Empty : ",")}");

                                if (columnAttribute.PrimaryKey)
                                {
                                    pkColNames += $"{(pkColNames.Length == 0 ? string.Empty : ",")}{columnAttribute.ColumnName}";
                                }
                            }
                                                        
                        });

                        if (pkColNames.Length > 0)
                        {
                            columnsDefinition.AppendLine($"\t, CONSTRAINT PK_{tableName} PRIMARY KEY ({pkColNames})");
                        }

                        var command = dbConnection.CreateCommand();
                        var query = new StringBuilder();
                        query.AppendLine($"IF NOT EXISTS(SELECT * FROM sys.tables t join sys.schemas s on (t.schema_id = s.schema_id) WHERE s.name = '{schemaName}' AND t.name = '{tableName}')");
                        query.AppendLine("BEGIN");
                        query.AppendLine($"CREATE TABLE {tableName} (");
                        query.AppendLine($"{columnsDefinition}");
                        query.AppendLine(")");
                        query.AppendLine("END");

                        command.CommandText = query.ToString();
                        command.ExecuteNonQuery();

                        logService.LogDebug($"[DatabaseOperationExtensions] Create table {tableName} sucess!");
                    }
                });
            }
            catch (Exception ex)
            {
                logService.LogException("[DatabaseOperationExtensions] Create Tables", ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private static string TableName(dynamic entity)
        {
            var attribute = Attribute.GetCustomAttribute(entity, typeof(EntityTable));
            if (attribute == null)
                throw new ArgumentException("TableName not defined in entity.");

            return attribute.TableName;
        }

    }
}
