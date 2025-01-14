using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Evento: IValidable
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool esCarga { get; set; }

        public void validar()
        {
            if (String.IsNullOrEmpty(nombre))
                throw new EventoException("El nombre de un evento no puede ser vacío o nulo.");
        }
    }
}
