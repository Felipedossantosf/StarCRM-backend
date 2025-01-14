using AccesoDatos.Interfaces;
using DTOs.Eventos;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Eventos
{
    public class AltaEvento : IAltaEvento
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public IRepositorioEventoComercial RepoEventoComercial { get; set; }
        public IRepositorioEventoUsuario RepoEventoUsuario { get; set; }

        public AltaEvento(IRepositorio<Evento> repoEvento, IRepositorioEventoComercial repoEventoComercial, IRepositorioEventoUsuario repoEventoUsuario)
        {
            RepoEvento = repoEvento;
            RepoEventoComercial = repoEventoComercial;
            RepoEventoUsuario = repoEventoUsuario;
        }

        public DTOCrearEvento Alta(DTOCrearEvento dtoEvento)
        {
            try
            {
                Evento nuevoEvento = new Evento()
                {
                    fecha = dtoEvento.fecha,
                    nombre = dtoEvento.nombre,
                    descripcion = dtoEvento.descripcion,
                    esCarga = dtoEvento.esCarga
                };

                RepoEvento.Add(nuevoEvento);
                dtoEvento.id = nuevoEvento.id;

                IEnumerable<EventoUsuario> eventosUsuario = dtoEvento.idUsuarios.Select(userId => new EventoUsuario()
                {
                    evento_id = nuevoEvento.id,
                    usuario_id = userId
                });
                RepoEventoUsuario.CrearEventoUsuarios(eventosUsuario);

                IEnumerable<EventoComercial> eventosComercial = dtoEvento.idComerciales.Select(comercialId => new EventoComercial()
                {
                    evento_id = nuevoEvento.id,
                    comercial_id = comercialId
                });
                RepoEventoComercial.CrearEventoComerciales(eventosComercial);

                return dtoEvento;
            }catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }catch(EventoException e)
            {
                throw new EventoException(e.Message);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }
    }
}
