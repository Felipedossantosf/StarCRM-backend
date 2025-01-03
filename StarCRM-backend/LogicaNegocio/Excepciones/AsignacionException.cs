using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class AsignacionException: Exception
    {
        public AsignacionException() { }    
        public AsignacionException(string message) : base(message) { }
        public AsignacionException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
