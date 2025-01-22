using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    [Table("LineaCotizacion")]
    public class LineaCotizacion
    {
        public int id { get; set; }
        public int cotizacion_id { get; set; }
        public Cotizacion cotizacion { get; set; } // configurar en contexto
        public int cant { get; set; }
        public decimal precioUnit { get; set; }
        public decimal totalLinea { get; set; }
    }
}
