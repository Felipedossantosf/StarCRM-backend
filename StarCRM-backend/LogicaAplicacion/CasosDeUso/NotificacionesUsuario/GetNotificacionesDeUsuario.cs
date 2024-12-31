using AccesoDatos.Interfaces;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.NotificacionesUsuario;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.NotificacionesUsuario
{
    public class GetNotificacionesDeUsuario : IGetNotificacionesDeUsuario
    {        
        public IRepositorioNotificacionUsuario RepoNotificacionUsuario { get; set; }

        public GetNotificacionesDeUsuario(IRepositorioNotificacionUsuario repoNotificacionUsuario)
        {            
            RepoNotificacionUsuario = repoNotificacionUsuario;
        }

        public IEnumerable<DTONotificacion> GetNotificacionesDe(int idUsuario)
        {
            IEnumerable<NotificacionUsuario> notificacionesUser = RepoNotificacionUsuario.ObtenerNotificacionesPorUsuario(idUsuario);
            IEnumerable<DTONotificacion> dtoNotificacioens = notificacionesUser.Select(nu => new DTONotificacion()
            {
                id = nu.notificacion.id,
                mensaje = nu.notificacion.mensaje,
                fecha = nu.notificacion.fecha,
                activa = nu.activa
            });

            return dtoNotificacioens;
        }
    }
}
