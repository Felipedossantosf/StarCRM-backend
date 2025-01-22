using DTOs.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Cotizaciones
{
    public interface IModificarCotizacion
    {
        DTOListarCotizacion Modificar(int id,DTOListarCotizacion dtoCotizacion);
    }
}
