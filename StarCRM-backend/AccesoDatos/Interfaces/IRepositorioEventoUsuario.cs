using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioEventoUsuario: IRepositorio<EventoUsuario>
    {
        void CrearEventoUsuarios(IEnumerable<EventoUsuario> eventoUsuarios);
        IEnumerable<int> GetUsuarioDeEvento(int eventoId);
    }
}
