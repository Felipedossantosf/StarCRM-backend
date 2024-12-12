using DTOs.Clientes;
using DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Clientes
{
    public interface IObtenerClientes
    {
        public IEnumerable<DTOCliente> GetAllClientes();
    }
}
