using DTOs.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Notificaciones
{
    public interface IAltaNotificacion
    {
        DTONotificacion Alta(DTONotificacion dtoNotificacion);
    }
}
