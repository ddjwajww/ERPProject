using Infrastructure.Models;
using SASSTS.DataAccess.DataAccess.Implementations;
using SASSTS.DataAccess.DataAccess.Interfaces;
using SASSTS.DataAccess.EntityFramework.Context;

namespace SASSTS.DataAccess.UnitOfWork
{
    public class UnitWork : IUnitWork
    {
        private Dictionary<Type, object> _repositories;
        private readonly SASSTSDataContext _context;

        public UnitWork(SASSTSDataContext context)
        {
            _repositories = new Dictionary<Type, object>();
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var result = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    result = true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return result;
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {

            if (_repositories.ContainsKey(typeof(IBaseRepository<TEntity>)))
            {
                return (IBaseRepository<TEntity>)_repositories[typeof(IBaseRepository<TEntity>)];
            }

            var repository = new BaseRepository<TEntity>(_context);
            _repositories.Add(typeof(IBaseRepository<TEntity>), repository);
            return repository;
        }

        #region Dispose
        bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}