using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Clientes
{
    public class DTOCliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Credito { get; set; }
        public string RazonSocial { get; set; }
        public string Rut { get; set; }
        public string Direccion { get; set; }
        public string SitioWeb { get; set; }
        public string TipoComercial { get; set; }
        public string Zafras { get; set; }
        public string Notas { get; set; }
        public DateTime? FechaUltCarga { get; set; }
        public bool? EsInactivo { get; set; }
        public string Estado { get; set; }

        // Id de usuario para registro de actividad
        public int usuario_id { get; set; }
    }
}
