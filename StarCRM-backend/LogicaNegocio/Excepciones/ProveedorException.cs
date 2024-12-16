using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{   
    public class ProveedorException: Exception
    {
        public ProveedorException() { }
        public ProveedorException(string message) : base(message) { }
        public ProveedorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
