using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.Notificaciones;
using LogicaAplicacion.Interfaces.NotificacionesUsuario;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        // Inyección de dependencias
        public IAltaNotificacionUsuario AltaNotificacionUsuario { get; set; }
        public IGetNotificacionesDeUsuario GetNotificacionesDeUsuario { get; set; }
        public IModificarNotificacionUsuario ModificarNotificacionUsuario { get; set; }
        // Constructor
        public NotificacionController(
          IAltaNotificacionUsuario altaNotificacionUsuario,
          IGetNotificacionesDeUsuario getNotificacionesDeUsuario,
          IModificarNotificacionUsuario modificarNotificacionUsuario
        )
        {
            AltaNotificacionUsuario = altaNotificacionUsuario;
            GetNotificacionesDeUsuario = getNotificacionesDeUsuario;
            ModificarNotificacionUsuario = modificarNotificacionUsuario;
        }


        /// <summary>
        /// Servicio que permite obtener listado de notificaciones de un usuario
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{usuarioId}")]
        public IActionResult ObtenerNotificaciones(int usuarioId)
        {
            try
            {
                var notificaciones = GetNotificacionesDeUsuario.GetNotificacionesDe(usuarioId);
                if (!notificaciones.Any())
                    return NotFound($"No se encontraron notificaciones para el usuario con id: {usuarioId}");
                
                return Ok(notificaciones);
            }
            catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al obtener las notificaciones.",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite crear notificacion para distintos usuarios
        /// </summary>
        /// <returns></returns>
        // POST api/<NotificacionController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOCrearNotificacion request)
        {

            try
            {
                DTONotificacion dtoNotificacion = new DTONotificacion()
                {
                    mensaje = request.mensaje,
                    fecha = DateTime.UtcNow,
                    cliente_id = request.cliente_id
                };


                AltaNotificacionUsuario.Alta(dtoNotificacion, request.usuariosId);

                return Ok("Notificación creada con éxito.");
            }
            catch(KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch(ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch(NotificacionException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al crear la notificación",
                    details = e.InnerException?.Message ?? e.Message
                });
            }
        }

        /// <summary>
        /// Servicio que permite cambiar estado de la notificación
        /// </summary>
        /// <returns></returns>
        // PUT api/<NotificacionController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTONotificacionUsuario dtoNotificacion)
        {
            if (dtoNotificacion == null)
                return BadRequest("El cuerpo de la solicitud no es válido.");

            try
            {
                dtoNotificacion = ModificarNotificacionUsuario.Modificar(id, dtoNotificacion);

                return Ok(new { message = "Notificacion modificada correctamente.", notificacion = dtoNotificacion });
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotificacionUsuarioException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mesage = "Ocurrió un error al actualizar la notificación", details = ex.Message });
            }
        }
        
    }
}
