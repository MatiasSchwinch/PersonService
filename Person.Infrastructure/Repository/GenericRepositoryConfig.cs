using Microsoft.EntityFrameworkCore;
using Person.Domain.SeedWork;
using System.Linq.Expressions;

namespace Person.Infrastructure.Repository
{
    public abstract class GenericRepositoryConfig<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

        public GenericRepositoryConfig(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<TEntity>();
        }

        protected async Task<IQueryable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            await Task.Run(
                () =>
                {
                    foreach (
                        var includeProperty in includeProperties.Split(
                            new char[] { ',', ';', '\u0020' },
                            StringSplitOptions.RemoveEmptyEntries
                        )
                    )
                    {
                        query = query.Include(includeProperty);
                    }
                }
            );

            return orderBy is null ? query : orderBy(query);
        }

        public async Task<IQueryable<TResult>> Get<TResult>(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Expression<Func<TEntity, TResult>>? selector = null,
            string includeProperties = ""
        )
        {
            return (await Get(filter, orderBy, includeProperties)).Select(
                selector ?? throw new ArgumentNullException(nameof(selector))
            );
        }

        //public IQueryable<T> Get<T>(Expression<Func<T, bool>>? filter = null) where T : Entity
        //{
        //    var query = _context.Set<T>();
        //    if (filter != null)
        //        query.Where(filter);
        //    return query;
        //}
    }
}
