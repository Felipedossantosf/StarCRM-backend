using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioLineaCotizacion: IRepositorio<LineaCotizacion>
    {
        IEnumerable<LineaCotizacion> GetLineasDeCotizacion(int cotizacion_id);
        void CrearLineasDeCotizacion(IEnumerable<LineaCotizacion> lineas);
        void EliminarLineasDeCotizacion(int cotizacion_id);
    }
}
