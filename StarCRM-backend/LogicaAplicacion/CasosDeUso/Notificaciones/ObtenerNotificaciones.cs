using AccesoDatos.Interfaces;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.Notificaciones;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Notificaciones
{
    public class ObtenerNotificaciones : IObtenerNotificaciones
    {
        public IRepositorio<Notificacion> RepoNotificacion { get; set; }
        public ObtenerNotificaciones(IRepositorio<Notificacion> repoNotificacion)
        {
            RepoNotificacion = repoNotificacion;
        }

        public IEnumerable<DTONotificacion> GetNotificaciones()
        {
            IEnumerable<Notificacion> notificaciones = RepoNotificacion.FindAll();
            IEnumerable<DTONotificacion> dtoNotificaciones = notificaciones.Select(n => new DTONotificacion()
            {
                id = n.id,
                mensaje = n.mensaje,
                fecha = n.fecha,
                cliente_id = n.cliente_id,
            });
            return dtoNotificaciones;
        }
    }
}
