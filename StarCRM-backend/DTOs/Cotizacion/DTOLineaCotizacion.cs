﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Cotizacion
{
    public class DTOLineaCotizacion
    {
        public int id { get; set; }
        public int cotizacion_id { get; set; }        
        public int cant { get; set; }
        public decimal precioUnit { get; set; }
        public decimal totalLinea { get; set; }
        public string? descripcion { get; set; }
        public int iva { get; set; }
    }
}
