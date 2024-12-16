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
    public class ObtenerProveedores : IObtenerProveedores
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public ObtenerProveedores(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public IEnumerable<DTOProveedor> GetAllProveedores()
        {
            IEnumerable<Comercial> proveedores = RepoComercial.FindAllProveedores();
            IEnumerable<DTOProveedor> dTOProveedores = proveedores.Select(c => new DTOProveedor()
            {
                Id = c.id,
                Nombre = c.nombre,
                Telefono = c.telefono,
                Correo = c.correo,
                Credito = c.credito,
                RazonSocial = c.razonSocial,
                Rut = c.rut,
                Direccion = c.direccion,
                SitioWeb = c.sitioWeb,
                TipoComercial = c.TipoComercial,
              
            });
            return dTOProveedores;
        }
    }
}
