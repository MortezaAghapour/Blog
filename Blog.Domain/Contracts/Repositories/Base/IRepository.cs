using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace Blog.Domain.Contracts.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        #region Get
        Task<T> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<TResult> Get<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<List<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<T> GetById(long id, CancellationToken cancellationToken = default);
        Task<T> GetWithIncludes(long id, CancellationToken cancellationToken = default);
        #endregion

        #region Insert
        Task Insert(T entity, CancellationToken cancellationToken = default);
        Task Insert(List<T> entities, CancellationToken cancellationToken = default);
        #endregion
        #region Update
        void Update(T entity);
        void Update(List<T> entities);
        #endregion
        #region Delete
        Task Delete(long id, CancellationToken cancellationToken = default);
        void Delete(T entity);
        void Delete(List<T> entities);
        Task DeleteWithIncludes(long id, CancellationToken cancellationToken = default);

        #endregion
    }
}