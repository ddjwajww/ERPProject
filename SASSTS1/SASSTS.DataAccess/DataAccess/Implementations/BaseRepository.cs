using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using SASSTS.DataAccess.DataAccess.Interfaces;
using SASSTS.DataAccess.EntityFramework.Context;
using System.Linq.Expressions;

namespace SASSTS.DataAccess.DataAccess.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly SASSTSDataContext _context;
        public BaseRepository(SASSTSDataContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            _context = dbContext;
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!, params string[] includeList)
        {
                IQueryable<TEntity> dbSet = _dbSet;

                if (includeList.Length > 0)
                {
                    foreach (var include in includeList)
                        dbSet = dbSet.Include(include);
                }

                if (predicate == null)
                    return await dbSet.ToListAsync();

               return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeList)
        {
                IQueryable<TEntity> dbSet = _dbSet;

                if (includeList.Length > 0)
                {
                    foreach (var include in includeList)
                        dbSet = dbSet.Include(include);
                }

                return (await dbSet.SingleOrDefaultAsync(predicate))!;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Result.Entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();   
        }

        public async Task<IQueryable<TEntity>> GetAllListAsync(params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Any())
            {
                foreach (var includeColumn in includeColumns)
                {
                    query = query.Include(includeColumn);
                }
            }
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<TEntity>> GetAllFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Length > 0)
            {
                foreach (var include in includeColumns)
                    query = query.Include(include);
            }
            return await Task.FromResult(query.Where(filter));
        }




        public async Task<IQueryable<TEntity>> GetNameFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Any())
            {
                foreach (var includeColumn in includeColumns)
                {
                    query = query.Include(includeColumn);
                }
            }
            return await Task.FromResult(query.Where(filter));
        }


        public async Task<IQueryable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Any())
            {
                foreach (var includeColumn in includeColumns)
                {
                    query = query.Include(includeColumn);
                }
            }
            return await Task.FromResult(query.Where(filter));
        }

        public async Task<TEntity> GetByNameFilterAsync(TEntity entity)
        {
            await _dbSet.FirstOrDefaultAsync<TEntity>();
            return entity;

        }

        public async Task<TEntity> GetSingleByFilterAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Any())
            {
                foreach (var includeColumn in includeColumns)
                {
                    query = query.Include(includeColumn);
                }
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetById(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }



        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Any())
            {
                foreach (var includeColumn in includeColumns)
                {
                    query = query.Include(includeColumn);
                }
            }
            return await _dbSet.AnyAsync(filter);
        }


        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }



        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var item = _dbSet.Find(id);
            _dbSet.Remove(item);
        }

        public async void DeleteAllItems(Expression<Func<TEntity, bool>> filter, params string[] includeColumns)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeColumns.Length > 0)
            {
                foreach (var include in includeColumns)
                    query = query.Include(include);
            }
            var numbers = await Task.FromResult(query.Where(filter));


            foreach (var item in numbers)
            {
                _dbSet.Remove(item);
            }
        }
    }
}