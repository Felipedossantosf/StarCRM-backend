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
    public class ObtenerEventos : IObtenerEventos
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public IRepositorioEventoUsuario RepoEventoUsuario { get; set; }
        public IRepositorioEventoComercial RepoEventoComercial { get; set; }
        public ObtenerEventos(IRepositorio<Evento> repoEvento, IRepositorioEventoUsuario repoEventoUsuario, IRepositorioEventoComercial repositorioEventoComercial)
        {
            RepoEvento = repoEvento;
            RepoEventoUsuario = repoEventoUsuario;
            RepoEventoComercial = repositorioEventoComercial;
        }
        public IEnumerable<DTOListarEvento> GetEventos()
        {
            IEnumerable<Evento> eventos = RepoEvento.FindAll();
            if (eventos == null || !eventos.Any())
                throw new KeyNotFoundException("No se encontraron eventos.");


            IEnumerable<DTOListarEvento> dTOListarEventos = eventos.Select(ev => new DTOListarEvento()
            {
                id = ev.id,
                fecha = ev.fecha,
                nombre = ev.nombre,
                descrpicion = ev.descripcion,
                esCarga = ev.esCarga,
                usuariosId = RepoEventoUsuario.GetUsuarioDeEvento(ev.id),
                comercialesId = RepoEventoComercial.GetComercialesDeEvento(ev.id)
            });
            return dTOListarEventos;
        }
    }
}
