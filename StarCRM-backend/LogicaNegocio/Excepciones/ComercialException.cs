using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class ComercialException: Exception
    {
        public ComercialException() { }
        public ComercialException(string message) : base(message) { }
        public ComercialException(string message, Exception innerException) : base(message, innerException) { }
    }
}
