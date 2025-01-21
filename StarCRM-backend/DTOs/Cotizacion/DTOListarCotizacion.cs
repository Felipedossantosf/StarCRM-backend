using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Cotizacion
{
    public class DTOListarCotizacion
    {
        public int id { get; set; }
        public int estado { get; set; }
        public string motivos { get; set; }
        public DateTime fecha { get; set; }
        public string metodosPago { get; set; }
        public string notas { get; set; }
        public double subtotal { get; set; }
        public int porcDesc { get; set; }
        public double subtotalDesc { get; set; }
        public int porcIva { get; set; }
        public double total { get; set; }
        public int cliente_id { get; set; }
        public int empresa_id { get; set; }
        public int usuario_id { get; set; }

        public IEnumerable<DTOLineaCotizacion> lineas { get; set; }
    }
}
