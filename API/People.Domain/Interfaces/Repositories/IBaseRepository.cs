using People.Domain.Entities;
using System.Linq.Expressions;

namespace People.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<K, T>
        where K : IComparable, IConvertible, IEquatable<K>
        where T : BaseEntity<K>
    {
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<bool> Delete(K id);
        public Task<T> FindById(K id);
        public Task<IList<T>> FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
