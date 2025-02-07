﻿using DTOs.Clientes;
using DTOs.Usuarios;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public IAltaCliente AltaCliente { get; set; }
        public IObtenerCliente ObtenerCliente { get; set; }
        public IObtenerClientes ObtenerClientes { get; set; }
        public IEliminarCliente EliminarCliente { get; set; }
        public IModificarCliente ModificarCliente { get; set; }
        public IObtenerClientesPerdidos ObtenerClientesPerdidos { get; set; }
        public IObtenerClientesLibres ObtenerClientesLibres { get; set; }
        public IObtenerClientesInactivos ObtenerClientesInactivos { get; set; }

        public ClienteController(
            IAltaCliente altaCliente,
            IObtenerCliente obtenerCliente,
            IObtenerClientes obtenerClientes,
            IEliminarCliente eliminarCliente,
            IModificarCliente modificarCliente,
            IObtenerClientesPerdidos obtenerClientesPerdidos,
            IObtenerClientesLibres obtenerClientesLibres,
            IObtenerClientesInactivos obtenerClientesInactivos
        )
        {
            AltaCliente = altaCliente;
            ObtenerCliente = obtenerCliente;
            ObtenerClientes = obtenerClientes;
            EliminarCliente = eliminarCliente;
            ModificarCliente = modificarCliente;
            ObtenerClientesPerdidos = obtenerClientesPerdidos;
            ObtenerClientesLibres = obtenerClientesLibres;
            ObtenerClientesInactivos = obtenerClientesInactivos;
        }


        /// <summary>
        /// Servicio que permite obtener listado de clientes
        /// </summary>
        /// <returns></returns>
        // GET: api/<ClienteController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<DTOCliente> dtoClientes = ObtenerClientes.GetAllClientes();
                return Ok(dtoClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar clientes", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener listado de clientes inactivos (perdidos)
        /// </summary>
        /// <returns></returns>
        // GET: api/<ClienteController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Perdidos")]
        public IActionResult GetPerdidos()
        {
            try
            {
                IEnumerable<DTOCliente> dtoClientes = ObtenerClientesPerdidos.GetClientesPerdidos();
                return Ok(dtoClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar clientes perdidos", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener listado de clientes cuya última carga tenga más de 6 meses de antiguedad
        /// </summary>
        /// <returns></returns>
        // GET: api/<ClienteController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Inactivos")]
        public IActionResult GetInactivos()
        {
            try
            {
                IEnumerable<DTOCliente> dtoClientes = ObtenerClientesInactivos.GetInactivos();
                return Ok(dtoClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar clientes inactivos", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener listado de clientes libres
        /// </summary>
        /// <returns></returns>
        // GET: api/<ClienteController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Libres")]
        public IActionResult GetLibres()
        {
            try
            {
                IEnumerable<DTOCliente> dtoClientes = ObtenerClientesLibres.GetClientesLibres();
                return Ok(dtoClientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al listar clientes perdidos", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite obtener un cliente por su id
        /// </summary>
        /// <returns></returns>
        // GET api/<ClienteController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "FindByIdCliente")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                DTOCliente dtoCliente = ObtenerCliente.ObtenerPorId(id);
                if (dtoCliente.Id == 0)
                {
                    return BadRequest();
                }
                return Ok(dtoCliente);
            }
            catch (ArgumentNullException nullExc)
            {
                return StatusCode(400, new { Message = "Ocurrió un error al buscar el cliente", Details = nullExc.Message });
            }
            catch (ComercialException comercialExc)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al buscar el cliente", Details = comercialExc.Message });
            }
            catch (ClienteException clienteExc)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al buscar el cliente", Details = clienteExc.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al buscar el cliente", Details = e.Message });
            }
        }


        /// <summary>
        /// Servicio que permite dar de alta un cliente
        /// </summary>
        /// <returns></returns>  
        // POST api/<ClienteController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] DTOCliente dtoCliente)
        {
            try
            {
                if (dtoCliente == null) return BadRequest("El cuerpo de la solicitud no es válido.");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                dtoCliente = AltaCliente.AltaCliente(dtoCliente);
                return CreatedAtRoute("FindByIdCliente", new { Id = dtoCliente.Id }, dtoCliente);
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
            catch (ClienteException clienteExc)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = clienteExc.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return Conflict(errorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al dar de alta un cliente", Details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite modificar un cliente
        /// </summary>
        /// <returns></returns>  
        // PUT api/<ClienteController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOCliente dtoCliente)
        {
            try
            {
                if (dtoCliente == null)
                    return BadRequest("El cuerpo de la solicitud no es válido.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                dtoCliente = ModificarCliente.Modificar(id, dtoCliente);

                return Ok(new { message = "Cliente actualizado correctamente.", cliente = dtoCliente });
            }
            catch (ClienteException e)
            {
                var errorResponse = new
                {
                    status = "error",
                    message = e.Message,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };
                return NotFound(errorResponse);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {mesage = "Ocurrió un error al actualizar al cliente", details = ex.Message });
            }
        }

        /// <summary>
        /// Servicio que permite eliminar un cliente
        /// </summary>
        /// <returns></returns> 
        // DELETE api/<ClienteController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] int usuario_id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Error al intentar borrar clienta, id inválido.");
                }
                EliminarCliente.Eliminar(id, usuario_id);
                // Agregar un encabezado personalizado con el mensaje                
                Response.Headers.Add("X-Message", "Cliente eliminado satisfactoriamente");
                return NoContent();
            }
            catch (ComercialException ex)
            {
                return StatusCode(404, new { Message = "Ocurrió un error al borrar cliente", Details = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {Message = "Ocurrió un error al borrar cliente", Details = ex.Message});
            }
        }
    }
}
