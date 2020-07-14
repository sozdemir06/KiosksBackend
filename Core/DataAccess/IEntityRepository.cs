using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Specifications;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> Add(T entity);
        Task<List<T>> AddRange(List<T> entities);
        Task<List<T>> UpdateRange(List<T> entities);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task DeleteRange(List<T> entities);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<List<T>> ListEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}