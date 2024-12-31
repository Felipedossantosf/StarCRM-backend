﻿using DTOs.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.NotificacionesUsuario
{
    public interface IGetNotificacionesDeUsuario
    {
        IEnumerable<DTONotificacion> GetNotificacionesDe(int idUsuario);
    }
}
