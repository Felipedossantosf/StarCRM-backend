using DTOs.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public IAltaUsuario AltaUsuario { get; set; }
        public ILogin LoginUsuario { get; set; }
        public IObtenerUsuario ObtenerUsuario { get; set; }
        public IObtenerUsuarios ObtenerUsuarios { get; set; }
        public IModificarUsuario ModificarUsuario { get; set; }

        public UsuarioController
        (
            IAltaUsuario altaUsuario,
            ILogin loginUsuario,
            IObtenerUsuario obtenerUsuario,
            IObtenerUsuarios obtenerUsuarios,
            IModificarUsuario modificarUsuario
        )
        {
            AltaUsuario = altaUsuario;
            LoginUsuario = loginUsuario;
            ObtenerUsuario = obtenerUsuario;
            ObtenerUsuarios = obtenerUsuarios;
            ModificarUsuario = modificarUsuario;
        }

        /// <summary>
        /// Servicio que permite obtener listado de usuarios
        /// </summary>
        /// <returns></returns>
        // GET: api/<UsuarioController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<DTOUsuarioRegistro> dtoUsuarios = ObtenerUsuarios.ObtenerUsuarios();
                return Ok(dtoUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Servicio que permite obtener un usuario por su Id
        /// </summary>
        /// <returns></returns>
        // GET api/<UsuarioController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "FindByIdUsuario")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                DTOUsuarioRegistro dtoUser = ObtenerUsuario.FindById(id);
                if (dtoUser.UserId == 0)
                {
                    return BadRequest();
                }
                return Ok(dtoUser);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Servicio que permite registrar un usuario
        /// </summary>
        /// <returns></returns>        
        // POST api/<Usuario>/Registro
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOUsuarioRegistro dtoUsuario)
        {
            try
            {
                if (dtoUsuario == null)
                {
                    return BadRequest("El cuerpo de la solicitud no es válido");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Devuelve las validaciones del modelo
                }
                dtoUsuario = AltaUsuario.Registrar(dtoUsuario);
                return CreatedAtRoute("FindByIdUsuario", new { Id = dtoUsuario.UserId }, dtoUsuario);
            }
            catch (UsuarioException ue)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = ue.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return Conflict(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al registrar el usuario", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite Loguear un usuario
        /// </summary>
        /// <returns></returns>        
        // POST api/<Usuario>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("login")]
        public IActionResult Post([FromBody] DTOLoginRequest dtoLoginRequest)
        {
            if (dtoLoginRequest == null) return BadRequest();
            DTOUsuarioLogin dtoUsuario = LoginUsuario.Login(dtoLoginRequest);
            if (String.IsNullOrEmpty(dtoUsuario.Username))
            {
                var errorResponse = new
                {
                    status = "error",
                    message = "Usuario no encontrado, credenciales incorrectas.",
                    timestampt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return NotFound(errorResponse);
            }

            DTOUsuarioLogueado dtoUsuarioLogueado = new DTOUsuarioLogueado()
            {
                Username = dtoUsuario.Username,
                Token = TokenManager.CrearToken(dtoUsuario),
                Rol = dtoUsuario.Rol,
                Nombre = dtoUsuario.Nombre,
                Apellido = dtoUsuario.Apellido
            };
            return Ok(dtoUsuarioLogueado);
        }

        /// <summary>
        /// Servicio que permite modificar un usuario
        /// </summary>
        /// <returns></returns>  
        // PUT api/<ClienteController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOUsuarioRegistro dtoUsuario)
        {
            try
            {
                if (dtoUsuario == null)
                    return BadRequest("El cuerpo de la solicitud no es válido.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verificar que el usuario exista
                var user = ObtenerUsuario.FindById(id);
                if (user == null)
                    return NotFound("Usuario no encontrado.");

                if (!BCrypt.Net.BCrypt.Verify(dtoUsuario.ContraseñaActual, user.Password))
                    return Unauthorized("Contraseña actual incorrecta.");

                // Realizar la modificación
                dtoUsuario = ModificarUsuario.Modificar(id, dtoUsuario);

                return Ok(new { message = "Usuario actualizado correctamente.", usuario = dtoUsuario });
            }
            catch (UsuarioException e)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = e.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar usuario", details = ex.Message });
            }
        }
    }
}



