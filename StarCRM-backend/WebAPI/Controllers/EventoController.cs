using DTOs.Eventos;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public IAltaEvento AltaEvento { get; set; }

        public EventoController(IAltaEvento altaEvento)
        {
            AltaEvento = altaEvento;
        }


        // GET: api/<EventoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EventoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Servicio que permite crear un evento con diferentes usuarios y comerciales vinculados
        /// </summary>
        /// <returns></returns>
        // POST api/<EventoController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOCrearEvento request)
        {
            try
            {
                AltaEvento.Alta(request);
                return Ok("Evento creado con éxito.");
            }catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }catch(EventoException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }catch(Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al dar de alta un nuevo evento.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        // PUT api/<EventoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
