using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    [Table("Cotizacion")]
    public class Cotizacion : IValidable
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

        public void validar()
        {
            if (estado == null)
                throw new CotizacionException("Estado de cotización no puede ser nulo.");
            if (empresa_id == null)
                throw new CotizacionException("Empresa de cotización no puede ser nula.");
        }
    }
}
