using DTOs.Asignaciones;
using DTOs.Eventos;
using LogicaAplicacion.CasosDeUso.Asignaciones;
using LogicaAplicacion.CasosDeUso.Eventos;
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
        public IObtenerEvento ObtenerEvento { get; set; }
        public IObtenerEventos ObtenerEventos { get; set; }
        public IModificarEvento ModificarEvento { get; set; }
        public IEliminarEvento EliminarEvento { get; set; }
        public EventoController(IAltaEvento altaEvento,
            IObtenerEvento obtenerEvento,
            IObtenerEventos obtenerEventos,
            IModificarEvento modificarEvento,
            IEliminarEvento eliminarEvento)
        {
            AltaEvento = altaEvento;
            ObtenerEvento = obtenerEvento;
            ObtenerEventos = obtenerEventos;
            ModificarEvento = modificarEvento;
            EliminarEvento = eliminarEvento;
        }


        /// <summary>
        /// Servicio que permite obtener listado de todos los eventos
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
                IEnumerable<DTOListarEvento> dtoEventos = ObtenerEventos.GetEventos();
                return Ok(dtoEventos);
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "404 Not Found", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar eventos", Details = e.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener un evento por su id
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
            if(id < 1)
                return StatusCode(400, new { Message = "Id ingresado inválido." });

            try
            {
                DTOListarEvento dtoEvento = ObtenerEvento.ObtenerPorId(id);
                if(dtoEvento.id == 0)
                    return StatusCode(404, new { Message = "Evento no encontrado." });

                return Ok(dtoEvento);
            }catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar el evento", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar el evento", Details = e.Message });
            }
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

        /// <summary>
        /// Servicio que permite actualizar un evento
        /// </summary>
        /// <returns></returns>
        // PUT api/<EventoController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOModificarEvento dtoEvento)
        {
            if (dtoEvento == null)
                return StatusCode(400, new { Message = "Evento ingresado nula." });

            try
            {
                dtoEvento = ModificarEvento.Modificar(id, dtoEvento);
                return Ok(new { message = "Evento modificado correctamente.", evento = dtoEvento });
            }
            catch (ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (AsignacionException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al modificar el evento.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite eliminar un evento
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
                EliminarEvento.Eliminar(id);
                Response.Headers.Add("X-Message", "Evento eliminado satisfactoriamente");
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
                    message = "Ocurrió un error al intentar eliminar el evento.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }
    }
}
