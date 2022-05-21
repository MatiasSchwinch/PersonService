using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;

namespace Person.Infrastructure.Repository
{
    public class PersonRepository : GenericRepositoryConfig<PersonEntity>, IPersonRepository
    {
        private readonly IMapper _mapper;

        public PersonRepository(PersonContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        private IQueryable<PersonEntity> PersonByIdQuery(int id)
        {
            return _entities.Where(person => person.PersonId == id);
        }

        public async Task<PersonEntityDto> GetPersonById(int id)
        {
            var person = await PersonByIdQuery(id)
                .Include(incl => incl.Location)
                .ThenInclude(tinc => tinc!.Coordinates)
                .Include(inc => inc.Location)
                .ThenInclude(tinc => tinc!.Timezone)
                .Include(incl => incl.Login)
                .Include(incl => incl.Picture)
                .Include(incl => incl.Registered)
                .FirstOrDefaultAsync();

            return person != null
              ? _mapper.Map<PersonEntityDto>(person)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<BasicDataDto> GetPersonBasicData(int personId)
        {
            var person = await PersonByIdQuery(personId).FirstOrDefaultAsync();

            return person != null
              ? _mapper.Map<BasicDataDto>(person)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<LocationDto> GetPersonLocation(int personId)
        {
            var location = await PersonByIdQuery(personId)
                .Include(incl => incl.Location)
                .ThenInclude(tinc => tinc!.Coordinates)
                .Include(inc => inc.Location)
                .ThenInclude(tinc => tinc!.Timezone)
                .Select(sel => sel.Location)
                .FirstOrDefaultAsync();

            return location != null
              ? _mapper.Map<LocationDto>(location)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<RegisteredDto> GetPersonRegistered(int personId)
        {
            var registered = await PersonByIdQuery(personId)
                .Include(incl => incl.Registered)
                .Select(sel => sel.Registered)
                .FirstOrDefaultAsync();

            return registered != null
              ? _mapper.Map<RegisteredDto>(registered)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<LoginDto> GetPersonLogin(int personId)
        {
            var loginInfo = await PersonByIdQuery(personId)
                .Include(incl => incl.Login)
                .Select(sel => sel.Login)
                .FirstOrDefaultAsync();

            return loginInfo != null
              ? _mapper.Map<LoginDto>(loginInfo)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<PictureDto> GetPersonPicture(int personId)
        {
            var pictures = await PersonByIdQuery(personId)
                .Include(incl => incl.Picture)
                .Select(sel => sel.Picture)
                .FirstOrDefaultAsync();

            return pictures != null
              ? _mapper.Map<PictureDto>(pictures)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<int> AddPerson(PersonEntityDto personDto)
        {
            var person = _mapper.Map<PersonEntity>(personDto);

            await _entities.AddAsync(person);
            await UnitOfWork.SaveChangesAsync();

            return person.PersonId;
        }

        public async Task DeletePerson(int id)
        {
            var person =
                await PersonByIdQuery(id).FirstOrDefaultAsync()
                ?? throw new EntityNotFoundException(
                    "La entidad no se pudo eliminar debido a que no se ha encontrado."
                );

            _entities.Remove(person);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdatePerson(PersonEntityDto personDto)
        {
            var person = _mapper.Map<PersonEntity>(personDto);
            _entities.Attach(person);
            _context.Entry(person).State = EntityState.Modified;

            await UnitOfWork.SaveChangesAsync();

            return person.PersonId;
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
