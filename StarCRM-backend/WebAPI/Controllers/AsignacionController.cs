using DTOs.Asignaciones;
using DTOs.Clientes;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.Asignaciones;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionController : ControllerBase
    {
        public IAltaAsignacion AltaAsignacion { get; set; }
        public IObtenerAsignacion ObtenerAsignacion { get; set; }
        public IModificarAsignacion ModificarAsignacion { get; set; }
        public IObtenerAsignaciones ObtenerAsignaciones { get; set; }
        public IEliminarAsignacion EliminarAsignacion { get; set; }

        public AsignacionController(
            IAltaAsignacion altaAsignacion,
            IObtenerAsignacion obtenerAsignacion,
            IModificarAsignacion modificarAsignacion,
            IObtenerAsignaciones obtenerAsignaciones,
            IEliminarAsignacion eliminarAsignacion
        )
        {
            AltaAsignacion = altaAsignacion;
            ObtenerAsignacion = obtenerAsignacion;
            ModificarAsignacion = modificarAsignacion;
            ObtenerAsignaciones = obtenerAsignaciones;
            EliminarAsignacion = eliminarAsignacion;
        }


        /// <summary>
        /// Servicio que permite obtener listado de todas las asignaciones
        /// </summary>
        /// <returns></returns>
        // GET: api/<AsignacionController>
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<DTOListarAsignacion> dtoAsignaciones = ObtenerAsignaciones.GetAsignaciones();
                return Ok(dtoAsignaciones);
            }catch(KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "404 Not Found", Details = e.Message });
            }catch(Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar asignaciones", Details = e.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener una asignación por su id
        /// </summary>
        /// <returns></returns>
        // GET api/<AsignacionController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "FindByIdAsignacion")]
        public IActionResult Get(int id)
        {
            try
            {
                if(id <= 0)
                    return StatusCode(400, new { Message = "Id ingresado inválido." });

                DTOListarAsignacion dtoAsignacion = ObtenerAsignacion.ObtenerPorId(id);
                if(dtoAsignacion.id == 0)
                    return StatusCode(404, new { Message = "Asignación no encontrada." });

                return Ok(dtoAsignacion);
            }
            catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar la asignacion", Details = e.Message });
            }catch(Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar la asignacion", Details = e.Message });
            }
        }

        /// <summary>
        /// Servicio que permite dar de alta una asignación
        /// </summary>
        /// <returns></returns>
        // POST api/<AsignacionController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOAsignacion dtoAsignacion)
        {
            try
            {
                AltaAsignacion.Alta(dtoAsignacion);
                return CreatedAtRoute("FindByIdAsignacion", new { id = dtoAsignacion.id }, dtoAsignacion);
            }
            catch(KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch(AsignacionException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al dar de alta una nueva asignación.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite actualizar una asignación
        /// </summary>
        /// <returns></returns>
        // PUT api/<AsignacionController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOModificarAsignacion dtoModificarAsignacion)
        {
            if(dtoModificarAsignacion == null)
                return StatusCode(400, new { Message = "Asignacion ingresada nula." });

            try
            {
                dtoModificarAsignacion = ModificarAsignacion.Modificar(id, dtoModificarAsignacion);
                return Ok(new { message = "Asignación modificada correctamente.", asignacion = dtoModificarAsignacion });
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
                    message = "Ocurrió un error al modificar la asignación.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite eliminar una asignación
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // DELETE api/<AsignacionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(id <= 0)
                return StatusCode(400, new { Message = "Asignacion id ingresado menor o igual a 0." });

            try
            {
                EliminarAsignacion.Eliminar(id);
                Response.Headers.Add("X-Message", "Asignacion eliminada satisfactoriamente");
                return NoContent();
            }
            catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch(AsignacionException e)
            {
                return StatusCode(400, new { Message = "Request inválida, revisar parametros.", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al intentar eliminar la asignación.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }

        }
    }
}
