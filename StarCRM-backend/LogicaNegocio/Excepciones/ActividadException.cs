using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class ActividadException: Exception
    {
        public ActividadException() { }
        public ActividadException(string message) : base(message) { }
        public ActividadException(string message, Exception innerException) : base(message, innerException) { }
    }
}
