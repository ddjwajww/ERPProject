using Infrastructure.Models;
using SASSTS.DataAccess.DataAccess.Interfaces;

namespace SASSTS.DataAccess.UnitOfWork
{
    public interface IUnitWork : IDisposable
    {
        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        public Task<bool> CommitAsync();
    }
}
