using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example1.Data.Abstractions
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Commit();
        Task CommitAsync();
        TEntity Delete(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        TEntity Delete(object id);
        Task<TEntity> DeleteAsync(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        TEntity GetFullObject(object id);
        Task<TEntity> GetFullObjectAsync(object id);
        IEnumerable<TEntity> GetPaged(int top = 20, int skip = 0, Expression<Func<TEntity, bool>> filter = null, object orderBy = null);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
