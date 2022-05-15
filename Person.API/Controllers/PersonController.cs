#nullable disable
using Microsoft.AspNetCore.Mvc;
using Person.Domain.PersonAggregate;
using System.Diagnostics;

namespace Person.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _repository;
        private readonly Stopwatch _stopwatch;

        public PersonController(ILogger<PersonController> logger, IPersonRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _stopwatch = new Stopwatch();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonEntity>>> GetAll()
        {
            _stopwatch.Start();
            var persons = await _repository.GetAll(
                person => person.Age <= 30 && person.Gender == 0 && person.Nationality == "GB",
                selector: person =>
                    new
                    {
                        person.PersonId,
                        person.FirstName,
                        person.LastName,
                        person.Location.State,
                        person.Location.City,
                        person.Login.Username,
                        person.Login.Password
                    }
            );
            _stopwatch.Stop();

            _logger.LogInformation("Query ejecutada en: {0} ms", _stopwatch.ElapsedMilliseconds);

            return Ok(persons);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonEntity>> GetById(int id)
        {
            _stopwatch.Start();
            var person = await _repository.FindById(id);
            _stopwatch.Stop();

            _logger.LogInformation("Query ejecutada en: {0} ms", _stopwatch.ElapsedMilliseconds);

            return Ok(person);
        }
    }
}
