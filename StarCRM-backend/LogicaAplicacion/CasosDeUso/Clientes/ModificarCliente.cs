using AccesoDatos.Interfaces;
using DTOs.Clientes;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Clientes
{
    public class ModificarCliente : IModificarCliente
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public ModificarCliente(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public DTOCliente Modificar(int id, DTOCliente dtoCliente)
        {            

            Cliente clienteBuscado = RepoComercial.FindById(id) as Cliente;

            if (clienteBuscado == null)
                throw new ClienteException($"No se encontró cliente con el id: {id}");

            try
            {
                clienteBuscado.nombre = dtoCliente.Nombre;
                clienteBuscado.telefono = dtoCliente.Telefono;
                clienteBuscado.correo = dtoCliente.Correo;
                clienteBuscado.credito = dtoCliente.Credito;
                clienteBuscado.razonSocial = dtoCliente.RazonSocial;
                clienteBuscado.rut = dtoCliente.Rut;
                clienteBuscado.direccion = dtoCliente.Direccion;
                clienteBuscado.sitioWeb = dtoCliente.SitioWeb;
                clienteBuscado.TipoComercial = dtoCliente.TipoComercial;
                clienteBuscado.zafras = dtoCliente.Zafras;
                clienteBuscado.notas = dtoCliente.Notas;
                clienteBuscado.esInactivo = dtoCliente.EsInactivo;
                clienteBuscado.estado = dtoCliente.Estado;

                RepoComercial.UpdateCliente(id, clienteBuscado);
                dtoCliente.Id = id;

                return dtoCliente;
            }
            catch (ClienteException e)
            {
                throw new ClienteException(e.Message);
            }
            catch( Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
