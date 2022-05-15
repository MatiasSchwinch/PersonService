using Person.Domain.SeedWork;
using System.Linq.Expressions;

namespace Person.Domain.PersonAggregate
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<IEnumerable<PersonEntity>> GetAll(
            Expression<Func<PersonEntity, bool>>? filter = null,
            Func<IQueryable<PersonEntity>, IOrderedQueryable<PersonEntity>>? orderBy = null,
            string includeProperties = ""
        );
        Task<IEnumerable<TResult>> GetAll<TResult>(
            Expression<Func<PersonEntity, bool>>? filter = null,
            Func<IQueryable<PersonEntity>, IOrderedQueryable<PersonEntity>>? orderBy = null,
            Expression<Func<PersonEntity, TResult>>? selector = null,
            string includeProperties = ""
        );
        Task<PersonEntity> FindById(int id, string includeProperties = "");
        Task Add(PersonEntity person);
        Task Delete(int id);
        Task Update(PersonEntity person);
    }
}
