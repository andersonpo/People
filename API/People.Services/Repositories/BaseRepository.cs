using Dapper;
using People.Domain.CustomAttributes;
using People.Domain.Entities;
using People.Domain.Interfaces.Repositories;
using People.Domain.Interfaces.Services;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;

namespace People.Services.Repositories
{
    public class BaseRepository<K, T> : IBaseRepository<K, T>
        where K : IComparable, IConvertible, IEquatable<K>
        where T : BaseEntity<K>
    {

        private readonly IDbConnection dbConnection;
        private readonly ILogService logService;
        public BaseRepository(IDbConnection dbConnection, ILogService logService)
        {
            this.dbConnection = dbConnection;
            this.logService = logService;
        }

        public virtual async Task<T> Create(T entity)
        {
            if (entity.Id == null)
            {
                throw new ArgumentNullException("Id");
            }

            var command = MapProperties(entity, false);
            var rowsAffected = await dbConnection.ExecuteAsync(command);
            logService.LogDebug($"[Insert] {TableName(typeof(T))} Success: {rowsAffected > 0}");
            return await FindById(entity.Id);
        }

        public virtual async Task<bool> Delete(K id)
        {
            var query = $"DELETE FROM {TableName(typeof(T))} WHERE Id = @id";
            var command = new CommandDefinition(query, new { id });
            var rowsAffected = await dbConnection.ExecuteAsync(command);
            logService.LogDebug($"[Delete] {TableName(typeof(T))} Success: {rowsAffected > 0}");
            return (rowsAffected > 0);
        }

        public virtual async Task<IList<T>> FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = $"SELECT * FROM {TableName(typeof(T))}";
            var command = new CommandDefinition(query);
            var rows = dbConnection.Query<T>(command);

            if (filter != null)
            {
                rows = rows.AsQueryable().Where(filter);
            }

            if (orderBy != null)
            {
                rows = orderBy(rows.AsQueryable());
            }

            logService.LogDebug($"[FindAll] {TableName(typeof(T))}");
            return rows.ToList();
        }

        public virtual async Task<T> FindById(K id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var query = $"SELECT * FROM {TableName(typeof(T))} WHERE Id =  @id";
            var command = new CommandDefinition(query, new { id });
            var row = dbConnection.QueryFirstOrDefault<T>(command);
            logService.LogDebug($"[FindById] {TableName(typeof(T))}");
            return row;
        }

        public virtual async Task<T> Update(T entity)
        {
            if (entity.Id == null)
            {
                throw new ArgumentNullException("Id");
            }

            var command = MapProperties(entity, true);
            var rowsAffected = await dbConnection.ExecuteAsync(command);
            
            logService.LogDebug($"[Update] {TableName(typeof(T))} Success: {rowsAffected > 0}");
            
            return await FindById(entity.Id);
        }

        public string TableName(Type entity)
        {
            var propsWithAttributes = (EntityTable[]?)Attribute.GetCustomAttributes(entity, typeof(EntityTable));
            if (propsWithAttributes == null || propsWithAttributes.Length < 0)
                throw new ArgumentException("TableName not defined in entity.");

            return propsWithAttributes[0].TableName;            
        }

        private CommandDefinition MapProperties(T entity, bool isUpdate)
        {
            var tableName = TableName(typeof(T));
            var allProps = entity.GetType().GetProperties();
            var propsEntityColumns = allProps.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(EntityColumn))).ToList();

            var queryProperties = new StringBuilder();
            var queryPropertiesValues = new StringBuilder();
            var queryPK = new StringBuilder();
            var parameters = new ExpandoObject() as IDictionary<string, Object?>; ;


            propsEntityColumns.ForEach(p =>
            {
                var isLast = propsEntityColumns.Last().Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase);
                var columnAttribute = ((EntityColumn[])p.GetCustomAttributes(typeof(EntityColumn), true)).FirstOrDefault<EntityColumn>();

                if (isUpdate)
                {
                    if (columnAttribute != null && columnAttribute.PrimaryKey)
                    {
                        queryPK.AppendFormat("{0} = @{0}{1}", p.Name, queryPK.Length > 0 ? "," : String.Empty);
                    }
                    else
                    {
                        queryProperties.AppendFormat("{0} = @{0}{1}", p.Name, isLast ? string.Empty : ", ");
                    }
                }
                else
                {
                    queryProperties.AppendFormat("{0}{1}", p.Name, isLast ? string.Empty : ", ");
                    queryPropertiesValues.AppendFormat("@{0}{1}", p.Name, isLast ? string.Empty : ", ");
                }

                parameters.Add(p.Name, p.GetValue(entity));
            });

            var query = $"INSERT INTO {tableName} ({queryProperties}) VALUES ({queryPropertiesValues})";
            if (isUpdate)
            {
                query = $"UPDATE {tableName} SET {queryProperties} WHERE {queryPK}";
            }
            
            var command = new CommandDefinition(query, parameters);

            return command;

        }
    }
}
