using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioEventoComercial: IRepositorio<EventoComercial>
    {
        void CrearEventoComerciales(IEnumerable<EventoComercial> events);
        IEnumerable<int> GetComercialesDeEvento(int eventoId);
        void EliminarPorEvento(int eventoId);
    }
}
