using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Example1.Data.Abstractions
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly TodoContext _context;
        protected DbSet<TEntity> dbSet;

        protected RepositoryBase(TodoContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return dbSet.Remove(entity).Entity;
        }

        public virtual Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removed = dbSet.Remove(entity);
            return Task.FromResult(removed.Entity);
        }

        public virtual TEntity Delete(object id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }

        public virtual async Task<TEntity> DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            return Delete(entity);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.AsNoTracking().Where(filter);
        }

        public TEntity GetById(object id)
        {
            var entity = dbSet.Find(id);
            if (entity == null) throw new NullReferenceException(nameof(entity));
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null) throw new NullReferenceException(nameof(entity));
            return entity;
        }

        public virtual TEntity GetFullObject(object id)
        {
            return GetById(id);
        }

        public async Task<TEntity> GetFullObjectAsync(object id)
        {
            return await GetByIdAsync(id);
        }

        public abstract IEnumerable<TEntity> GetPaged(int top = 20, int skip = 0, Expression<Func<TEntity, bool>> filter = null, object orderBy = null);

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public Task InsertAsync(TEntity entity)
        {
            dbSet.Add(entity);
            return Task.FromResult(0);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(0);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_context != null) _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoryBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
