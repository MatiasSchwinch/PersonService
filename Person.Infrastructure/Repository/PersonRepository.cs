using Microsoft.EntityFrameworkCore;
using Person.Domain.PersonAggregate;
using Person.Domain.SeedWork;
using System.Linq.Expressions;

namespace Person.Infrastructure.Repository
{
    public class PersonRepository : GenericGetQuery<PersonEntity>, IPersonRepository
    {
        private readonly PersonContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public PersonRepository(PersonContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Get Genérico IQueryable
        private async Task<IQueryable<TEntity>> Get<TEntity>(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = ""
        ) where TEntity : Entity
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

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
        #endregion

        public async Task<IEnumerable<PersonEntity>> GetAll(
            Expression<Func<PersonEntity, bool>>? filter = null,
            Func<IQueryable<PersonEntity>, IOrderedQueryable<PersonEntity>>? orderBy = null,
            string includeProperties = ""
        )
        {
            return await (await Get(filter, orderBy, includeProperties)).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(
            Expression<Func<PersonEntity, bool>>? filter = null,
            Func<IQueryable<PersonEntity>, IOrderedQueryable<PersonEntity>>? orderBy = null,
            Expression<Func<PersonEntity, TResult>>? selector = null,
            string includeProperties = ""
        )
        {
            return await (await Get(filter, orderBy, includeProperties))
                .Select(selector ?? throw new ArgumentNullException(nameof(selector)))
                .ToListAsync();
        }

        public async Task<PersonEntity> FindById(int id, string includeProperties = "")
        {
            return (
                await Get<PersonEntity>(
                    person => person.PersonId == id,
                    includeProperties: includeProperties
                )
            ).FirstOrDefault()!;
        }

        public async Task Add(PersonEntity person)
        {
            await _context.BasicData.AddAsync(person);
        }

        public async Task Delete(int id)
        {
            _context.BasicData.Remove(
                await FindById(id)
                    ?? throw new EntityNotFoundException(
                        "La entidad no se pudo eliminar debido a que no se ha encontrado."
                    )
            );
        }

        public async Task Update(PersonEntity person)
        {
            await Task.Run(
                () =>
                {
                    _context.BasicData.Attach(person);
                    _context.Entry(person).State = EntityState.Modified;
                }
            );
        }

        #region Implementación IDisposable
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
