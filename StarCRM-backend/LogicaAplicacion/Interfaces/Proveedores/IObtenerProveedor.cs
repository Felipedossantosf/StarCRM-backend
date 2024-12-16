using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Proveedor;

namespace LogicaAplicacion.Interfaces.Proveedor
{
    public interface IObtenerProveedor
    {
        DTOProveedor ObtenerPorId(int id);

    }
}
