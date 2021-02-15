using Microsoft.EntityFrameworkCore;
using ProjectManagement.EntityFramework.Shared.DbContexts;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagement.EntityFramework.Shared.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public virtual async Task<T> AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public virtual async Task<T> UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetListAsync()
        {
            var result= await _dbSet
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {

            try
            {
                var query = (IQueryable<T>) _dbSet.Where(predicate);
                return await includes.Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty)).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Delete(T item)
        {
            _dbSet.Remove(item);
            await _db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(string id)
        {
            var item = await GetByIdAsync(id);
            await Delete(item);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}