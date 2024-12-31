using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioNotificacionUsuario: IRepositorio<NotificacionUsuario>
    {
        IEnumerable<NotificacionUsuario> ObtenerNotificacionesPorUsuario(int userId);
        void CrearNotificacionesUsuario(IEnumerable<NotificacionUsuario> notificacionesUsuario);
    }
}
