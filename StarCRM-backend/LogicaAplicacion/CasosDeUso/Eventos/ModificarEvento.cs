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
    public class ModificarEvento : IModificarEvento
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public ModificarEvento(IRepositorio<Evento> repoEvento)
        {
            RepoEvento = repoEvento;
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
    }
}
