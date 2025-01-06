using DTOs.Actividades;
using LogicaAplicacion.Interfaces.Actividades;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        public IObtenerActividad ObtenerActividad { get; set; }
        public IObtenerActividades ObtenerActividades { get; set; }

        public ActividadController(IObtenerActividad obtenerActividad, IObtenerActividades obtenerActividades)
        {
            ObtenerActividad = obtenerActividad;
            ObtenerActividades = obtenerActividades;
        }


        /// <summary>
        /// Servicio que permite obtener listado del historial de actividades
        /// </summary>
        /// <returns></returns>
        // GET: api/<ActividadController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<DTOListarActividad> dtoAsignaciones = ObtenerActividades.getAllActividades();
                return Ok(dtoAsignaciones);
            }
            catch(KeyNotFoundException e)
            {
                return StatusCode(404, new { Message = "404 Not Found", Details = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar historial de actividades", Details = e.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener una actividad por su id
        /// </summary>
        /// <returns></returns>
        // GET api/<ActividadController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id <= 0)
                return StatusCode(400, new { Message = "Id ingresado inválido." });

            try
            {
                DTOListarActividad dtoActividad = ObtenerActividad.GetPorId(id);
                if(dtoActividad.id == 0)
                    return StatusCode(404, new { Message = "Asignación no encontrada." });

                return Ok(dtoActividad);
            }
            catch(ArgumentNullException e)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar la actividad", Details = e.Message });
            }catch(Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar la actividad", Details = e.InnerException?.Message ?? e.Message });
            }
        }
        
    }
}
