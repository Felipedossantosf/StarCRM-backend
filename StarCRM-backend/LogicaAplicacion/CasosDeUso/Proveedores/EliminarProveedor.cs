using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Proveedor
{
    public class EliminarProveedor : IEliminarProveedor
    {
        public IRepositorioComercial RepoComercial { get; set; }

        public EliminarProveedor(IRepositorioComercial repoComercial) 
        {
            RepoComercial = repoComercial;
        }

        public void Eliminar(int id) 
        {
            if (id <= 0)
            {
                throw new ComercialException("El ID debe ser mayor a cero");
            }

            try
            {
                RepoComercial.Remove(id);
            }
            catch (ComercialException ex)
            {
                throw new ComercialException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new ComercialException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ComercialException(ex.Message);
            }
        }
    }
}
