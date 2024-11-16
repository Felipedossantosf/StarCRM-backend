using DTOs.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
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

        public UsuarioController
        (
            IAltaUsuario altaUsuario,
            ILogin loginUsuario,
            IObtenerUsuario obtenerUsuario
        )
        {
            AltaUsuario = altaUsuario;
            LoginUsuario = loginUsuario;
            ObtenerUsuario = obtenerUsuario;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOUsuarioRegistro dtoUsuario)
        {
            try
            {
                if (dtoUsuario == null)
                {
                    return BadRequest();
                }
                dtoUsuario = AltaUsuario.Registrar(dtoUsuario);
                return CreatedAtRoute("FindByIdUsuario", new { Id = dtoUsuario.UserId }, dtoUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
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
            if (dtoUsuario == null)
            {
                return NotFound($"Usuario no encontrado: {dtoLoginRequest.username}");
            }
            
            DTOUsuarioLogueado dtoUsuarioLogueado = new DTOUsuarioLogueado()
            {
                Username = dtoUsuario.Username,
                Token = TokenManager.CrearToken(dtoUsuario)
            };
            return Ok(dtoUsuarioLogueado);                        
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
