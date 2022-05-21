#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;
using System.Diagnostics;

namespace Person.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;
        private readonly Stopwatch _stopwatch;

        public PersonController(
            ILogger<PersonController> logger,
            IPersonRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _stopwatch = new Stopwatch();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonEntityDto>> GetPerson(int id)
        {
            try
            {
                _stopwatch.Start();
                var person = await _repository.GetPersonById(id);
                _stopwatch.Stop();

                _logger.LogInformation(
                    "Query ejecutada en: {0} ms",
                    _stopwatch.ElapsedMilliseconds
                );

                return Ok(person);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpGet("{id:int}/Location")]
        public async Task<ActionResult<LocationDto>> GetPersonLocation(int id)
        {
            try
            {
                _stopwatch.Start();
                var personLocation = await _repository.GetPersonLocation(id);
                _stopwatch.Stop();

                _logger.LogInformation(
                    "Query ejecutada en: {0} ms",
                    _stopwatch.ElapsedMilliseconds
                );

                return Ok(personLocation);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpGet("{id:int}/Registered")]
        public async Task<ActionResult<RegisteredDto>> GetPersonRegistered(int id)
        {
            try
            {
                _stopwatch.Start();
                var personRegistered = await _repository.GetPersonRegistered(id);
                _stopwatch.Stop();

                _logger.LogInformation(
                    "Query ejecutada en: {0} ms",
                    _stopwatch.ElapsedMilliseconds
                );

                return Ok(personRegistered);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpGet("{id:int}/Login")]
        public async Task<ActionResult<LoginDto>> GetPersonLogin(int id)
        {
            try
            {
                _stopwatch.Start();
                var personLogin = await _repository.GetPersonRegistered(id);
                _stopwatch.Stop();

                _logger.LogInformation(
                    "Query ejecutada en: {0} ms",
                    _stopwatch.ElapsedMilliseconds
                );

                return Ok(personLogin);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpGet("{id:int}/Picture")]
        public async Task<ActionResult<PictureDto>> GetPersonPicture(int id)
        {
            try
            {
                _stopwatch.Start();
                var personPicture = await _repository.GetPersonPicture(id);
                _stopwatch.Stop();

                _logger.LogInformation(
                    "Query ejecutada en: {0} ms",
                    _stopwatch.ElapsedMilliseconds
                );

                return Ok(personPicture);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonEntityDto>> PostPerson([FromBody] PersonEntityDto person)
        {
            try
            {
                var id = await _repository.AddPerson(person);

                return Ok($"El registro fue añadido correctamente a la base de datos con el id: {id}.");
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                await _repository.DeletePerson(id);

                return Ok(new ResponseSuccess($"La entidad con 'id: {id}' se elimino correctamente de la base de datos."));
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseException(ex.Message, ex.GetType().ToString()));
            }
        }
    }
}
