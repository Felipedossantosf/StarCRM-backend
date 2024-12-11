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

        public UsuarioController
        (
            IAltaUsuario altaUsuario,
            ILogin loginUsuario,
            IObtenerUsuario obtenerUsuario,
            IObtenerUsuarios obtenerUsuarios
        )
        {
            AltaUsuario = altaUsuario;
            LoginUsuario = loginUsuario;
            ObtenerUsuario = obtenerUsuario;
            ObtenerUsuarios = obtenerUsuarios;
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
            }catch(Exception ex)
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
                if(id <= 0)
                {
                    return BadRequest();
                }
                DTOUsuarioRegistro dtoUser = ObtenerUsuario.FindById(id);
                if(dtoUser.UserId == 0)
                {
                    return BadRequest();
                }
                return Ok(dtoUser);
            }catch(Exception e)
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
            catch(UsuarioException ue)
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

        //// PUT api/<UsuarioController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
