using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Proveedor
{
    public interface IEliminarProveedor
    {
        void Eliminar(int id, int usuario_id);
    }
}
