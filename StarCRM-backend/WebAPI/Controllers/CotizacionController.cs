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
        public IObtenerCotizaciones ObtenerCotizaciones { get; set; }
        public IEliminarCotizacion EliminarCotizacion { get; set; }
        public IModificarCotizacion ModificarCotizacion { get; set; }
        public CotizacionController(
            IGetCotizacion getCotizacion,
            IAltaCotizacion altaCotizacion,
            IObtenerCotizaciones obtenerCotizaciones,
            IEliminarCotizacion eliminarCotizacion,
            IModificarCotizacion modificarCotizacion)
        {
            GetCotizacion = getCotizacion;
            AltaCotizacion = altaCotizacion;
            ObtenerCotizaciones = obtenerCotizaciones;
            EliminarCotizacion = eliminarCotizacion;
            ModificarCotizacion = modificarCotizacion;
        }


        /// <summary>
        /// Servicio que permite obtener listado de todas las cotizaciones
        /// </summary>
        /// <returns></returns>
        // GET: api/<EventoController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<DTOListarCotizacion> dtoCotizaciones = ObtenerCotizaciones.GetAllCotizaciones();
                return Ok(dtoCotizaciones);
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "404 Not Found", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar cotizaciones", Details = e.Message });
            }
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
        [HttpGet("{id}", Name = "FindByIdCotizacion")]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOListarCotizacion dtoCotizacion)
        {
            try
            {
                AltaCotizacion.Alta(dtoCotizacion);
                return CreatedAtRoute("FindByIdCotizacion", new { Id = dtoCotizacion.id }, dtoCotizacion);
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

        /// <summary>
        /// Servicio que permite actualizar una cotizacion y sus lineas
        /// </summary>
        /// <returns></returns>
        // PUT api/<EventoController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOListarCotizacion dtoCotizacion)
        {
            if (dtoCotizacion == null)
                return StatusCode(400, new { Message = "Cotización ingresada nula." });

            try
            {
                dtoCotizacion = ModificarCotizacion.Modificar(id, dtoCotizacion);
                return Ok(new { message = "Cotización modificada correctamente.", cotizacion = dtoCotizacion });
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al modificar la cotización.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite eliminar una cotizacion y sus lineas
        /// </summary>
        /// <returns></returns>
        // DELETE api/<EventoController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return StatusCode(400, new { Message = "Evento id ingresado menor o igual a 0." });

            try
            {
                EliminarCotizacion.Eliminar(id);
                Response.Headers.Add("X-Message", "Cotizacion eliminada satisfactoriamente");
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al intentar eliminar la cotización.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }
    }
}
