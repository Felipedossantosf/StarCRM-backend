using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Excepciones;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Proveedor;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {   
        public IAltaProveedor AltaProveedor { get; set; }

        public IObtenerProveedor ObtenerProveedor {  set; get; }

        public IObtenerProveedores ObtenerProveedores { set; get; }

        public IEliminarProveedor EliminarProveedor { get; set; }

        public IModificarProveedor ModificarProveedor { get; set; }

        public ProveedorController(
        IAltaProveedor altaProveedor,
        IObtenerProveedor obtenerProveedor,
        IObtenerProveedores obtenerProveedores,
        IEliminarProveedor eliminarProveedor,
        IModificarProveedor modificarProveedor
        )
        {
            AltaProveedor = altaProveedor;
            ObtenerProveedor= obtenerProveedor;
            ObtenerProveedores = obtenerProveedores;
            EliminarProveedor = eliminarProveedor;
            ModificarProveedor = modificarProveedor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult get()
        {
            try
            {
                IEnumerable<DTOProveedor> dtoProveedores = ObtenerProveedores.GetAllProveedores();
                return Ok(dtoProveedores);
            }
            catch ( Exception ex ) 
                {
                    return StatusCode(500, new { Message = "Ocurrió un error al listar proveedores", Details = ex.Message });
                }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "FindByIdProveedor")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                DTOProveedor dTOProveedor = ObtenerProveedor.ObtenerPorId(id);
                if (dTOProveedor.Id == 0)
                {
                    return BadRequest();
                }
                return Ok(dTOProveedor);
            }
            catch (ArgumentNullException nullExc)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar el proveedor", Details = nullExc.Message });
            }
            catch (ComercialException comercialExc)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al buscar el proveedor", Details = comercialExc.Message });
            }
            catch (ProveedorException proveedorExc)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al buscar el proveedor", Details = proveedorExc.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar el proveedor", Details = e.Message });
            }
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOProveedor dTOProveedor)
        {
            try
            {
                if (dTOProveedor == null) return BadRequest("El cuerpo de la solicitud no es válido.");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                dTOProveedor = AltaProveedor.AltaProveedor(dTOProveedor);
                return CreatedAtRoute("FindByIdProveedor", new { id = dTOProveedor.Id }, dTOProveedor);
            }
            catch (ComercialException comercialExc)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = comercialExc.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return Conflict(errorResponse);
            }
            catch (ProveedorException proveedorExc)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = proveedorExc.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return Conflict(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al dar de alta un Proveedor", Details = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOProveedor dTOProveedor)
        {
            try
            {
                if (dTOProveedor == null) return BadRequest("El cuerpo de la solicitud no es válido.");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                dTOProveedor = ModificarProveedor.Modificar(id, dTOProveedor);

                return Ok(new { message = "Proveedor Actualizado correctamente", proveedor = dTOProveedor });
            }
            catch (ProveedorException e)
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
                return StatusCode(500, new { mesage = "Ocurrió un error al actualizar al proveedor", details = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Error al intentar borrar clienta, id inválido.");
                EliminarProveedor.Eliminar(id);
                Response.Headers.Add("X-Message", "Proveedor eliminado satisfactoriamente");
                return Ok(new { message = "Proveedor Eliminado correctamente", proveedor = id });


            }
            catch (ComercialException ex)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al borrar proveedor", Details = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al borrar proveedor", Details = ex.Message });
            }
        }
    }
}
