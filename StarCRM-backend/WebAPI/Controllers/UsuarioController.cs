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

        public UsuarioController
        (
            IAltaUsuario altaUsuario,
            ILogin loginUsuario
        )
        {
            AltaUsuario = altaUsuario;
            LoginUsuario = loginUsuario;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
                return CreatedAtRoute("FindByIdUsuario", new { Id = dtoUsuario.Id }, dtoUsuario);
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
        [HttpPost("/Login")]
        public IActionResult Post([FromBody] DTOUsuarioLogin dtoUsuario)
        {
            if (dtoUsuario == null) return BadRequest();
            dtoUsuario = LoginUsuario.Login(dtoUsuario);
            if (dtoUsuario.Id > 0)
            {
                DTOUsuarioLogueado dtoUsuarioLogueado = new DTOUsuarioLogueado()
                {
                    FullName = dtoUsuario.FullName,
                    Token = TokenManager.CrearToken(dtoUsuario)
                };
                return Ok(dtoUsuarioLogueado);
            }
            return NotFound();
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
