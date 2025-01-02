using AccesoDatos.Interfaces;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.NotificacionesUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.NotificacionesUsuario
{
    public class AltaNotificacionUsuario : IAltaNotificacionUsuario
    { 
        public IRepositorioNotificacionUsuario RepoNotificacionUsuario { get; set; }
        public IRepositorio<Notificacion> RepoNotificacion { get; set; }
        public IRepositorioComercial RepoComercial { get; set; } 
        public AltaNotificacionUsuario(
            IRepositorioNotificacionUsuario repoNotificacionUsuario,
            IRepositorio<Notificacion> repoNotificacion,
            IRepositorioComercial repoComercial)
        {
            RepoNotificacionUsuario = repoNotificacionUsuario;
            RepoNotificacion = repoNotificacion;
            RepoComercial = repoComercial;
        }

        public DTONotificacion Alta(DTONotificacion dtoNotificacion, IEnumerable<int> idUsuarios)
        {
            try
            {
                var clienteBuscado = RepoComercial.FindByCondition(c => c.id == dtoNotificacion.cliente_id && c.TipoComercial == "Cliente")
                                        .FirstOrDefault();

                Cliente cliente = clienteBuscado as Cliente;

                if (cliente == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con el ID {dtoNotificacion.cliente_id}");
                }

                Notificacion nuevaNotificacion = new Notificacion()
                {
                    mensaje = dtoNotificacion.mensaje,
                    fecha = dtoNotificacion.fecha,
                    cliente_id = dtoNotificacion.cliente_id
                };

                RepoNotificacion.Add(nuevaNotificacion);
                dtoNotificacion.id = nuevaNotificacion.id;

                IEnumerable<NotificacionUsuario> notificacionesUsuario = idUsuarios.Select(userId => new NotificacionUsuario()
                {
                    usuario_id = userId,
                    notificacion_id = nuevaNotificacion.id,
                    activa = true,
                    notificacion = nuevaNotificacion
                });

                RepoNotificacionUsuario.CrearNotificacionesUsuario(notificacionesUsuario);

                return dtoNotificacion;
            }
            catch(NotificacionException e)
            {
                throw new NotificacionException(e.Message);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        
    }
}
