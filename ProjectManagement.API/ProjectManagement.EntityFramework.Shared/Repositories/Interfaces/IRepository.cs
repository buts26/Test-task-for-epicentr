using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> GetByIdAsync(string id); //todo refact
        Task<T> GetByIdAsync(int id); //todo refact
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task Delete(T item);
        Task DeleteAsync(string id);
        Task SaveAsync();
    }
}
