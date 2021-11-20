using People.Domain.DTOs.Requests;
using System.Linq.Expressions;

namespace People.Domain.Interfaces.Services
{
    public interface IBaseService<K, T>
        where K : IComparable, IConvertible, IEquatable<K>
        where T : BaseRequestDTO<K>
    {
        public Task<T> Create(T dto);
        public Task<T> Update(T dto);
        public Task<bool> Delete(K id);
        public Task<T> FindById(K id);
        public Task<IList<T>> FindAll();
        public Task<IList<T>> FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
    }
}
