using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class EventoException: Exception
    {
        public EventoException() { }
        public EventoException(string message) : base(message) { }
        public EventoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
