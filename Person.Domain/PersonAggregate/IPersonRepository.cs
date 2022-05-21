using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;

namespace Person.Domain.PersonAggregate
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<PersonEntityDto> GetPersonById(int id);
        Task<BasicDataDto> GetPersonBasicData(int personId);
        Task<LocationDto> GetPersonLocation(int personId);
        Task<RegisteredDto> GetPersonRegistered(int personId);
        Task<LoginDto> GetPersonLogin(int personId);
        Task<PictureDto> GetPersonPicture(int personId);
        Task<int> AddPerson(PersonEntityDto personDto);
        Task DeletePerson(int id);
        Task<int> UpdatePerson(PersonEntityDto personDto);
    }
}
