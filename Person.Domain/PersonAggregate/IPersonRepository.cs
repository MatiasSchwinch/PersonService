using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;

namespace Person.Domain.PersonAggregate
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<IEnumerable<BasicDataDto>> GetAllAsync(string[] filters);
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
