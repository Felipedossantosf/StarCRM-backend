using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Clientes
{
    public class EliminarCliente : IEliminarCliente
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public EliminarCliente(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public void Eliminar(int id)
        {
            RepoComercial.Remove(id);
        }
    }
}
