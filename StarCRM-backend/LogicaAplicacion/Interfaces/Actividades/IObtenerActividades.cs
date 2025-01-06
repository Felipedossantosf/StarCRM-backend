using DTOs.Actividades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Actividades
{
    public interface IObtenerActividades
    {
        IEnumerable<DTOListarActividad> getAllActividades();
    }
}
