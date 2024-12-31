using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class NotificacionUsuarioException: Exception
    {
        public NotificacionUsuarioException() { }
        public NotificacionUsuarioException(string message) : base(message) { }
        public NotificacionUsuarioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
