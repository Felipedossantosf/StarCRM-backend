using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
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
    public class AltaProveedor : IAltaProveedor
    {
        public IRepositorioComercial RepoComercial { get; set; }

        public AltaProveedor(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }
        DTOProveedor  IAltaProveedor.AltaProveedor(DTOProveedor dTOProveedor)
        {
            if(dTOProveedor == null)
                throw new ArgumentNullException(nameof(dTOProveedor));
           

            Comercial nuevoProveedor = new Comercial()
            {
                nombre = dTOProveedor.Nombre,
                telefono = dTOProveedor.Telefono,
                correo = dTOProveedor.Correo,
                credito = dTOProveedor.Credito,
                razonSocial = dTOProveedor.RazonSocial,
                rut = dTOProveedor.Rut,
                direccion = dTOProveedor.Direccion,
                sitioWeb = dTOProveedor.SitioWeb,
                TipoComercial = dTOProveedor.TipoComercial,
               
            };

            

            try
            {
                RepoComercial.Add(nuevoProveedor);
                dTOProveedor.Id = nuevoProveedor.id;
            }
            catch (ComercialException comExc)
            {
                throw new ComercialException(comExc.Message);
            }
            catch (ProveedorException cliExc)
            {
                throw new ProveedorException(cliExc.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dTOProveedor;
        }
    }
}
