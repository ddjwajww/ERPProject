using Infrastructure.Models;
using System.Linq.Expressions;

namespace SASSTS.DataAccess.DataAccess.Interfaces
{
    public interface IBaseRepository<TEntity> 
        where TEntity : class, IEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!, params string[] includeList);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList);
        Task<TEntity> InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllListAsync(params string[] includeColumns);
        Task<IQueryable<TEntity>> GetAllFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns);
        Task<IQueryable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns);
        Task<TEntity> GetByNameFilterAsync(TEntity entity);
        Task<TEntity> GetSingleByFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns);
        Task<TEntity> GetById(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        void DeleteAllItems(Expression<Func<TEntity, bool>> filter, params string[] includeColumns);
    }
}
