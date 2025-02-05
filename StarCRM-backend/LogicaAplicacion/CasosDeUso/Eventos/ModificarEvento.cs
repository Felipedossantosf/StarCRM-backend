using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
using DTOs.Eventos;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Eventos
{
    public class ModificarEvento : IModificarEvento
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public IRepositorioEventoUsuario RepoEventoUsuario { get; set; }
        public IRepositorioEventoComercial RepoEventoComercial { get; set; }
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public ModificarEvento(IRepositorio<Evento> repoEvento,
            IRepositorioEventoUsuario repoEventoUsuario,
            IRepositorioEventoComercial repositorioEventoComercial,
            IRepositorio<Actividad> repoActividad)
        {
            RepoEvento = repoEvento;
            RepoEventoUsuario = repoEventoUsuario;
            RepoEventoComercial = repositorioEventoComercial;
            RepoActividad = repoActividad;
        }
        public DTOModificarEvento Modificar(int id, DTOModificarEvento dtoEvento)
        {
            Evento eventoBuscado = RepoEvento.FindById(id);
            if (eventoBuscado == null)
                throw new EventoException($"No se encontró el evento con el id: {id}");
            

            try
            {
                eventoBuscado.fecha = dtoEvento.fecha;
                eventoBuscado.nombre = dtoEvento.nombre;
                eventoBuscado.descripcion = dtoEvento.descripcion;
                eventoBuscado.esCarga = dtoEvento.esCarga;
                

                RepoEvento.Update(id, eventoBuscado);
                
                dtoEvento.id = id;

                // Actualizar las relaciones con usuarios
                ActualizarUsuarios(id, dtoEvento.usuariosId);

                // Actualizar las relaciones con comerciales
                ActualizarComerciales(id, dtoEvento.comercialesId);

                Actividad nuevaActividad = new Actividad()
                {
                    fecha = DateTime.Now,
                    descripcion = $"Se modificó el evento: {dtoEvento.nombre}",
                    usuario_id = dtoEvento.usuario_id
                };
                RepoActividad.Add(nuevaActividad);
                    
                return dtoEvento;
            }catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (EventoException e)
            {
                throw new EventoException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ActualizarUsuarios(int eventoId, IEnumerable<int> nuevosUsuariosId)
        {
            // Eliminar las relaciones existentes
            RepoEventoUsuario.EliminarPorEvento(eventoId);

            // Insertar las nuevas relaciones
            foreach (var usuarioId in nuevosUsuariosId)
            {
                RepoEventoUsuario.Add(new EventoUsuario
                {
                    evento_id = eventoId,
                    usuario_id = usuarioId
                });
            }
        }

        private void ActualizarComerciales(int eventoId, IEnumerable<int> nuevosComercialesId)
        {
            // Eliminar las relaciones existentes
            RepoEventoComercial.EliminarPorEvento(eventoId);

            // Insertar las nuevas relaciones
            foreach (var comercialId in nuevosComercialesId)
            {
                RepoEventoComercial.Add(new EventoComercial
                {
                    evento_id = eventoId,
                    comercial_id = comercialId
                });
            }
        }
    }
}
