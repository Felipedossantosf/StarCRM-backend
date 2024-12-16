using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaAplicacion.Interfaces.Proveedor;
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
            RepoComercial.Remove(id);
        }
    }
}
