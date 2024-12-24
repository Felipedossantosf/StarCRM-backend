using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Interfaces;
using DTOs.Proveedor;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.CasosDeUso.Proveedor
{
    public class ObtenerProveedor : IObtenerProveedor
    {
        public IRepositorioComercial RepoComercial { get; set; }

        public ObtenerProveedor(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public DTOProveedor ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            var proveedorBuscado = RepoComercial.FindByCondition(c => c.id == id && c.TipoComercial == "Proveedor")
                                        .FirstOrDefault();

            Comercial proveedor = proveedorBuscado as Comercial;

            if (proveedor == null)
            {
                throw new KeyNotFoundException($"No se encontró un proveedor con el ID {id}");
            }

            DTOProveedor dTOProveedor = new DTOProveedor()
            {
                Id = proveedor.id,
                Nombre = proveedor.nombre,
                Telefono = proveedor.telefono,
                Correo = proveedor.correo,
                Credito = proveedor.credito,
                RazonSocial = proveedor.razonSocial,
                Rut = proveedor.rut,
                Direccion = proveedor.direccion,
                SitioWeb = proveedor.sitioWeb,
                TipoComercial = proveedor.TipoComercial,
            };

            return dTOProveedor;
        }

    }
}
