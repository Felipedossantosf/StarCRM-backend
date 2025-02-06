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

        public IEnumerable<DTOListarNotificacion> GetNotificacionesDe(int idUsuario)
        {
            try
            {
                IEnumerable<NotificacionUsuario> notificacionesUser = RepoNotificacionUsuario.ObtenerNotificacionesPorUsuario(idUsuario);
                if (notificacionesUser == null)
                    throw new KeyNotFoundException($"No se encontraron notificaciones para el usuario con id: {idUsuario}");

                IEnumerable<DTOListarNotificacion> dtoNotificacioens = notificacionesUser.Select(nu => new DTOListarNotificacion()
                {
                    notificacion_usuario_id = nu.id,
                    notificacion_id = nu.notificacion.id,
                    mensaje = nu.notificacion.mensaje,
                    fecha = nu.notificacion.fecha,
                    cliente_id = nu.notificacion.cliente_id,
                    activa = nu.activa
                });

                return dtoNotificacioens;
            }
            catch(ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
