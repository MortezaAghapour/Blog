using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Contracts.Repositories.Base;
using Blog.Domain.Entities.Base;
using Blog.Infrastructure.ApplicationContexts;
using Blog.Infrastructure.Extensions.DbContexts;
using Blog.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Blog.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        #region Fields

        private readonly BlogDbContext _blogDbContext;
        protected DbSet<T> Entities => _blogDbContext.Set<T>();
        public IQueryable<T> Table => Entities.AsQueryable();
        public IQueryable<T> TableAsNoTracking => Entities.AsNoTracking();
        #endregion
        #region Constructors
        public Repository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        #endregion

        #region Methods

        #region Get
        public async Task<T> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false,
                CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TResult> Get<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<T> GetById(long id, CancellationToken cancellationToken = default)
        {
            if (id <=0)
            {
                throw new NotFoundException($"the {nameof(id)} not less than equal zero in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            // return await Entities.FindAsync(new object[]{ id }, cancellationToken);
            return await Entities.FindAsync(new object[]{id},cancellationToken);
        }

        public async Task<T> GetWithIncludes(long id, CancellationToken cancellationToken = default) 
        {
            if (id <=0)
            {
                throw new NotFoundException($"the {nameof(id)} not less than equal zero in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            var includes = _blogDbContext.Model.GetIncludePaths(typeof(T));
            var query = Entities.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(c =>c.Id ==id,cancellationToken);
        }

        #endregion

        #region Insert
        public async Task Insert(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            await Entities.AddAsync(entity, cancellationToken);
        }

        public async Task Insert(List<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
            {
                throw new NotFoundException($"the {nameof(entities)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            await Entities.AddRangeAsync(entities, cancellationToken);
        }


        #endregion
        #region Update
        public void Update(T entity)
        {
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }  
            Entities.Update(entity);
        }

        public void Update(List<T> entities)
        {
            if (entities is null)
            {
                throw new NotFoundException($"the {nameof(entities)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            Entities.UpdateRange(entities);
        }

        #endregion


        #region Delete
        public async Task Delete(long id, CancellationToken cancellationToken = default)
        {
            if (id <=0)
            {
                throw new NotFoundException($"the {nameof(id)} not less than equal zero in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            var entity = await GetById(id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }

            Entities.Remove(entity);
        }

        public void Delete(T entity)
        {
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            Entities.Remove(entity);
        }

        public void Delete(List<T> entities)
        {
            if (entities is null)
            {
                throw new NotFoundException($"the {nameof(entities)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            Entities.RemoveRange(entities);
        }

        public async Task DeleteWithIncludes(long id, CancellationToken cancellationToken = default)
        {
            if (id <=0)
            {
                throw new NotFoundException($"the {nameof(id)} not less than equal zero in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }

            var query = Entities.AsQueryable();
            var includes = _blogDbContext.Model.GetIncludePaths(typeof(T));
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var foundObject =await query.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (foundObject is null)
            {
                throw new NotFoundException($"the {nameof(foundObject)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }

            Entities.Remove(foundObject);
        }

        #endregion


        #endregion
    }
}