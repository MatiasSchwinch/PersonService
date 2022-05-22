using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;
using System.Linq.Expressions;

namespace Person.Domain.PersonAggregate
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<IEnumerable<PersonEntityDto>> GetAllAsync(Expression<Func<PersonEntity, bool>>? filter);
        Task<PersonEntityDto> GetPersonByIdAsync(int id);
        Task<BasicDataDto> GetPersonBasicDataAsync(int personId);
        Task<LocationDto> GetPersonLocationAsync(int personId);
        Task<RegisteredDto> GetPersonRegisteredAsync(int personId);
        Task<LoginDto> GetPersonLoginAsync(int personId);
        Task<PictureDto> GetPersonPictureAsync(int personId);
        Task<int> AddPersonAsync(PersonEntityDto personDto);
        Task DeletePersonAsync(int id);
        Task<int> UpdatePersonAsync(PersonEntityDto personDto);
    }
}
