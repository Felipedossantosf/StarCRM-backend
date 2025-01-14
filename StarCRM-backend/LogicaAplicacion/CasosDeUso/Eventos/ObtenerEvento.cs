using AccesoDatos.Interfaces;
using DTOs.Eventos;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Eventos
{
    public class ObtenerEvento : IObtenerEvento
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public IRepositorioEventoUsuario RepoEventoUsuario { get; set; }
        public IRepositorioEventoComercial RepoEventoComercial { get; set; }
        public ObtenerEvento(IRepositorio<Evento> repoEvento, IRepositorioEventoUsuario repoEventoUsuario, IRepositorioEventoComercial repoEventoComercial)
        {
            RepoEvento = repoEvento;
            RepoEventoUsuario = repoEventoUsuario;
            RepoEventoComercial = repoEventoComercial;
        }

        public DTOListarEvento ObtenerPorId(int id)
        {
            try
            {
                Evento eventoBuscado = RepoEvento.FindById(id);

                IEnumerable<int> idUsuarios = RepoEventoUsuario.GetUsuarioDeEvento(id);
                IEnumerable<int> idComerciales = RepoEventoComercial.GetComercialesDeEvento(id);

                DTOListarEvento dtoEvento = new DTOListarEvento()
                {
                    id = eventoBuscado.id,
                    fecha = eventoBuscado.fecha,
                    nombre = eventoBuscado.nombre,
                    descrpicion = eventoBuscado.descripcion,
                    esCarga = eventoBuscado.esCarga,
                    usuariosId = idUsuarios,
                    comercialesId = idComerciales
                };

                return dtoEvento;
            }catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
