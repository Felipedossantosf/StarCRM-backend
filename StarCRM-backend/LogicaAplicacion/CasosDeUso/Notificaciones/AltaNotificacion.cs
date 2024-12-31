using AccesoDatos.Interfaces;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.Notificaciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Notificaciones
{
    public class AltaNotificacion : IAltaNotificacion
    {
        public IRepositorio<Notificacion> RepoNotificacion { get; set; }
        public AltaNotificacion(IRepositorio<Notificacion> repoNotificacion)
        {
            RepoNotificacion = repoNotificacion;
        }

        public DTONotificacion Alta(DTONotificacion dtoNotificacion)
        {
            if (dtoNotificacion == null)
                throw new ArgumentNullException(nameof(dtoNotificacion));

            Notificacion newNotificacion = new Notificacion()
            {
                mensaje = dtoNotificacion.mensaje,
                fecha = dtoNotificacion.fecha,
                cliente_id = dtoNotificacion.cliente_id
            };

            try
            {
                RepoNotificacion.Add(newNotificacion);
                dtoNotificacion.id = newNotificacion.id;
                return dtoNotificacion;
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch(NotificacionException e)
            {
                throw new NotificacionException(e.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
