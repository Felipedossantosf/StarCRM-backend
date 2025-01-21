using DTOs.Cotizacion;
using DTOs.Eventos;
using LogicaAplicacion.CasosDeUso.Eventos;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        public IGetCotizacion GetCotizacion { get; set; }   
        public IAltaCotizacion AltaCotizacion { get; set; }
        public CotizacionController(
            IGetCotizacion getCotizacion,
            IAltaCotizacion altaCotizacion)
        {
            GetCotizacion = getCotizacion;
            AltaCotizacion = altaCotizacion;
        }


        // GET: api/<CotizacionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Servicio que permite obtener una cotización por su id
        /// </summary>
        /// <returns></returns>
        // GET api/<EventoController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 1)
                return StatusCode(400, new { Message = "Id ingresado inválido." });

            try
            {
                DTOListarCotizacion dtoCotizacion = GetCotizacion.GetCotizacionPorId(id);
                if (dtoCotizacion.id == 0)
                    return StatusCode(404, new { Message = "Evento no encontrado." });

                return Ok(dtoCotizacion);
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar la cotización", Details = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al buscar la cotización", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar la cotización", Details = e.Message });
            }
        }

        /// <summary>
        /// Servicio que permite crear una cotización
        /// </summary>
        /// <returns></returns>
        // POST api/<EventoController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOListarCotizacion dtoCotizacion)
        {
            try
            {
                AltaCotizacion.Alta(dtoCotizacion);
                return Ok("Cotización creada con éxito.");
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (CotizacionException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al dar de alta una nueva cotización.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        // PUT api/<CotizacionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CotizacionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
