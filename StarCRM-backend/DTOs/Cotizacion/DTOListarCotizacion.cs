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
        public string estado { get; set; }        
        public DateTime fecha { get; set; }
        public string metodosPago { get; set; }        
        public double subtotal { get; set; }
        public int porcDesc { get; set; }
        public double subtotalDesc { get; set; }
        public int porcIva { get; set; }
        public double total { get; set; }
        public int cliente_id { get; set; }
        public int empresa_id { get; set; }
        public int usuario_id { get; set; }
        public int proveedor_id { get; set; }
        public DateTime fechaValidez { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string condicionFlete { get; set; }
        public string modo { get; set; } 
        public string mercaderia { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public string terminosCondiciones { get; set; }
        public string tipo { get; set; }

        public IEnumerable<DTOLineaCotizacion> lineas { get; set; }                
    }
}
