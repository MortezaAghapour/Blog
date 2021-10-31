using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Contracts.UnitOfWorks;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Shared.Exceptions;
using Blog.Shared.Markers.DependencyInjectionLifeTimes;
using Blog.Shared.Resources;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork ,IScopedLifeTime
    {
        #region Fields

        private readonly BlogDbContext _blogDbContext;
        private IDbContextTransaction _dbContextTransaction;


        #endregion
        #region Constructors
        public UnitOfWork(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;

        }
        #endregion
        #region Methods
        public void BeginTransaction()
        {
            _dbContextTransaction = _blogDbContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await _blogDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void CommitTransaction()
        {
            if (_dbContextTransaction == null)
            {
                throw new NullReferenceException(ErrorResources.BeginTransactionCallError);
            }
            _dbContextTransaction.Commit();
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction == null)
            {
                throw new NullReferenceException(ErrorResources.BeginTransactionCallError);
            }

            await _dbContextTransaction.CommitAsync(cancellationToken);
        }

        public void RollbackTransaction()
        {
            if (_dbContextTransaction == null)
            {
                throw new NullReferenceException(ErrorResources.BeginTransactionCallError);
            }
            _dbContextTransaction.Rollback();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContextTransaction == null)
            {
                throw new NullReferenceException(ErrorResources.BeginTransactionCallError);
            }
            await _dbContextTransaction.RollbackAsync(cancellationToken);
        }

        public int SaveChange()
        {
            return _blogDbContext.SaveChanges();
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _blogDbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

    }
}