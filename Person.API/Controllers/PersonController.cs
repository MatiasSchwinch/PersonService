using Microsoft.AspNetCore.Mvc;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;
using Person.Domain.SeedWork;

namespace Person.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _repository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Retorna el registro completo de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonEntityDto>> GetPerson(int id)
        {
            try
            {
                var person = await _repository.GetPersonById(id);

                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPerson Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Retorna solo la información básica de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}/BasicData")]
        public async Task<ActionResult<BasicDataDto>> GetPersonBasicData(int id)
        {
            try
            {
                var personLocation = await _repository.GetPersonBasicData(id);

                return Ok(personLocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPersonBasicData Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Retorna solo la información de la ubicación de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}/Location")]
        public async Task<ActionResult<LocationDto>> GetPersonLocation(int id)
        {
            try
            {
                var personLocation = await _repository.GetPersonLocation(id);

                return Ok(personLocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPersonLocation Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Retorna solo la información del registro de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}/Registered")]
        public async Task<ActionResult<RegisteredDto>> GetPersonRegistered(int id)
        {
            try
            {
                var personRegistered = await _repository.GetPersonRegistered(id);

                return Ok(personRegistered);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPersonRegistered Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Retorna solo la información de credenciales de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}/Login")]
        public async Task<ActionResult<LoginDto>> GetPersonLogin(int id)
        {
            try
            {
                var personLogin = await _repository.GetPersonRegistered(id);

                return Ok(personLogin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPersonLogin Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Retorna solo las imágenes de una persona realizando la búsqueda mediante su Id.
        /// </summary>
        /// <param name="id">(int) Numero de Id del registro</param>
        /// <returns></returns>
        [HttpGet("{id:int}/Picture")]
        public async Task<ActionResult<PictureDto>> GetPersonPicture(int id)
        {
            try
            {
                var personPicture = await _repository.GetPersonPicture(id);

                return Ok(personPicture);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPersonPicture Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Añade un nuevo registro completo a la base de datos.
        /// </summary>
        /// <param name="person">(PersonEntityDto) Entidad que se va a guardar en la base de datos.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PersonEntityDto>> PostPerson(
            [FromBody] PersonEntityDto person
        )
        {
            try
            {
                var id = await _repository.AddPerson(person);

                return Ok(
                    $"El registro fue añadido correctamente a la base de datos y se le asigno el id nro: {id}."
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PostPerson Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Edita un registro existente en la base de datos.
        /// </summary>
        /// <param name="person">(PersonEntityDto) Entidad que se va a editar en la base de datos.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<PersonEntityDto>> PutPerson(
            [FromBody] PersonEntityDto person
        )
        {
            try
            {
                var id = await _repository.UpdatePerson(person);

                return Ok($"El registro con 'id: {id}' fue actualizado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PutPerson Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }

        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <param name="id">Numero de id del registro a eliminar.</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                await _repository.DeletePerson(id);

                return Ok(
                    new ResponseSuccess(
                        $"La entidad con 'id: {id}' se elimino correctamente de la base de datos."
                    )
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletePerson Method");
                return NotFound(
                    new ResponseException(
                        ex.Message,
                        ex.GetType().ToString(),
                        ex.StackTrace,
                        ex.InnerException
                    )
                );
            }
        }
    }
}
