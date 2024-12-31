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
    public class ModificarNotificacionUsuario : IModificarNotificacionUsuario
    {
        public IRepositorioNotificacionUsuario RepoNotificacionUsuario { get; set; }
        public ModificarNotificacionUsuario(IRepositorioNotificacionUsuario repoNotificacionUsuario)
        {
            RepoNotificacionUsuario = repoNotificacionUsuario;
        }

        public DTONotificacionUsuario Modificar(int id, DTONotificacionUsuario dtoNotificacionUsuario)
        {
            NotificacionUsuario notificacionBuscada = RepoNotificacionUsuario.FindById(id);

            if (notificacionBuscada == null)
                throw new NotificacionUsuarioException($"No se encontró NotificacionUsuario con el id: {id}");

            try
            {
                notificacionBuscada.activa = dtoNotificacionUsuario.activa;

                RepoNotificacionUsuario.Update(id, notificacionBuscada);
                dtoNotificacionUsuario.id = id;

                return dtoNotificacionUsuario;
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch(NotificacionUsuarioException ex)
            {
                throw new NotificacionUsuarioException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
