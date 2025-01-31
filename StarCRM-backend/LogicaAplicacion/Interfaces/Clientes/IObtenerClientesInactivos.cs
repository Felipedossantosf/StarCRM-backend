using DTOs.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Clientes
{
    public interface IObtenerClientesInactivos
    {
        IEnumerable<DTOCliente> GetInactivos();
    }
}
