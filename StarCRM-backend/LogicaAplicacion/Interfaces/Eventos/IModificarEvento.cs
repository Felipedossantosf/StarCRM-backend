using DTOs.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Eventos
{
    public interface IModificarEvento
    {
        DTOModificarEvento Modificar(int id, DTOModificarEvento dtoEvento);
    }
}
