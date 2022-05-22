using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<PersonEntityDto>> GetAllAsync(Expression<Func<PersonEntity, bool>>? filter = null)
        {
            return _mapper.Map<IEnumerable<PersonEntityDto>>(await (await Get(filter)).ToListAsync());
        }

        public async Task<PersonEntityDto> GetPersonByIdAsync(int id)
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

        public async Task<BasicDataDto> GetPersonBasicDataAsync(int personId)
        {
            var person = await PersonByIdQuery(personId).FirstOrDefaultAsync();

            return person != null
              ? _mapper.Map<BasicDataDto>(person)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<LocationDto> GetPersonLocationAsync(int personId)
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

        public async Task<RegisteredDto> GetPersonRegisteredAsync(int personId)
        {
            var registered = await PersonByIdQuery(personId)
                .Include(incl => incl.Registered)
                .Select(sel => sel.Registered)
                .FirstOrDefaultAsync();

            return registered != null
              ? _mapper.Map<RegisteredDto>(registered)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<LoginDto> GetPersonLoginAsync(int personId)
        {
            var loginInfo = await PersonByIdQuery(personId)
                .Include(incl => incl.Login)
                .Select(sel => sel.Login)
                .FirstOrDefaultAsync();

            return loginInfo != null
              ? _mapper.Map<LoginDto>(loginInfo)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<PictureDto> GetPersonPictureAsync(int personId)
        {
            var pictures = await PersonByIdQuery(personId)
                .Include(incl => incl.Picture)
                .Select(sel => sel.Picture)
                .FirstOrDefaultAsync();

            return pictures != null
              ? _mapper.Map<PictureDto>(pictures)
              : throw new EntityNotFoundException("La entidad no se encontró.");
        }

        public async Task<int> AddPersonAsync(PersonEntityDto personDto)
        {
            var person = _mapper.Map<PersonEntity>(personDto);

            await _entities.AddAsync(person);
            await UnitOfWork.SaveChangesAsync();

            return person.PersonId;
        }

        public async Task DeletePersonAsync(int id)
        {
            var person =
                await PersonByIdQuery(id).FirstOrDefaultAsync()
                ?? throw new EntityNotFoundException(
                    "La entidad no se pudo eliminar debido a que no se ha encontrado."
                );

            _entities.Remove(person);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdatePersonAsync(PersonEntityDto personDto)
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
