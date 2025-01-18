using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class CotizacionException:Exception
    {
        public CotizacionException() { }
        public CotizacionException(string message) : base(message) { }
        public CotizacionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
