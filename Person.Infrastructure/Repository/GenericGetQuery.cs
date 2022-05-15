using Person.Domain.SeedWork;

namespace Person.Infrastructure.Repository
{
    public abstract class GenericGetQuery<TEntity> where TEntity : Entity
    {
        //private readonly DbContext _context;

        //public GenericGetQuery(DbContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}

        //private async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null,
        //                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //                                            string includeProperties = "")
        //{
        //    IQueryable<TEntity> query = _context.Set<TEntity>();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    await Task.Run(() =>
        //    {
        //        foreach (var includeProperty in includeProperties.Split(new char[] { ',', ';', '\u0020' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    });

        //    return orderBy is null ? query
        //                           : orderBy(query);
        //}
    }
}
