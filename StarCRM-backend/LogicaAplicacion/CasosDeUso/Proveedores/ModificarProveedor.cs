using AccesoDatos.Interfaces;
using DTOs.Clientes;
using DTOs.Proveedor;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Proveedor
{
    public class ModificarProveedor : IModificarProveedor
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public ModificarProveedor(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public DTOProveedor Modificar(int id, DTOProveedor dTOProveedor)
        {

            Comercial proveedorBuscado = RepoComercial.FindById(id) as Comercial;

            if (proveedorBuscado == null)
                throw new ProveedorException($"No se encontró Proveedor con el id: {id}");

            try
            {
                proveedorBuscado.nombre = dTOProveedor.Nombre;
                proveedorBuscado.telefono = dTOProveedor.Telefono;
                proveedorBuscado.correo = dTOProveedor.Correo;
                proveedorBuscado.credito = dTOProveedor.Credito;
                proveedorBuscado.razonSocial = dTOProveedor.RazonSocial;
                proveedorBuscado.rut = dTOProveedor.Rut;
                proveedorBuscado.direccion = dTOProveedor.Direccion;
                proveedorBuscado.sitioWeb = dTOProveedor.SitioWeb;
                proveedorBuscado.TipoComercial = dTOProveedor.TipoComercial;
           

                RepoComercial.Update(id, proveedorBuscado);
                dTOProveedor.Id = id;

                return dTOProveedor;
            }
            catch (ProveedorException e)
            {
                throw new ProveedorException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
