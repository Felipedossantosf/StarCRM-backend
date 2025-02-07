using AccesoDatos.Interfaces;
using DTOs.Clientes;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Clientes
{
    public class ObtenerCliente : IObtenerCliente
    {
        public IRepositorioComercial RepoComercial { get; set; }

        public ObtenerCliente(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public DTOCliente ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            var clienteBuscado = RepoComercial.FindByCondition(c => c.id == id && c.TipoComercial == "Cliente")
                                        .FirstOrDefault();

            Cliente cliente = clienteBuscado as Cliente;

            if (cliente == null)
            {
                throw new KeyNotFoundException($"No se encontró un cliente con el ID {id}");
            }

            DTOCliente dtoCliente = new DTOCliente()
            {
                Id = cliente.id,
                Nombre = cliente.nombre,
                Telefono = cliente.telefono,
                Correo = cliente.correo,
                Credito = cliente.credito,
                RazonSocial = cliente.razonSocial,
                Rut = cliente.rut,
                Direccion = cliente.direccion,
                SitioWeb = cliente.sitioWeb,
                TipoComercial = cliente.TipoComercial,
                FechaUltCarga = cliente.fechaUltCarga,
                Zafras = cliente.zafras,
                Notas = cliente.notas,
                EsInactivo = cliente.esInactivo,
                Estado = cliente.estado
            };

            return dtoCliente;
        }
    }
}
