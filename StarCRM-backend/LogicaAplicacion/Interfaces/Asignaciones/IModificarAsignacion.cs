using DTOs.Asignaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Asignaciones
{
    public interface IModificarAsignacion
    {
        DTOModificarAsignacion Modificar(int id, DTOModificarAsignacion dtoModificarAsignacion);
    }
}
