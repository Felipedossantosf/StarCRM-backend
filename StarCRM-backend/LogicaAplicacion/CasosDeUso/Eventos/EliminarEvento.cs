using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Eventos
{
    public class EliminarEvento : IEliminarEvento
    {
        public IRepositorio<Evento> RepoEvento { get; set; }
        public IRepositorioEventoComercial RepoEventoComercial { get; set; }
        public IRepositorioEventoUsuario RepoEventoUsuario { get; set; }
        public EliminarEvento(IRepositorio<Evento> repoEvento, IRepositorioEventoComercial repoEventoComercial, IRepositorioEventoUsuario repoEventoUsuario)
        {
            RepoEvento = repoEvento;
            RepoEventoComercial = repoEventoComercial;
            RepoEventoUsuario = repoEventoUsuario;
        }

        public void Eliminar(int id)
        {
            if(id <= 0)
                throw new ArgumentNullException("Id recibido por parámetro inválido.");

            try
            {
                RepoEventoComercial.EliminarPorEvento(id);
                RepoEventoUsuario.EliminarPorEvento(id);
                RepoEvento.Remove(id);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
