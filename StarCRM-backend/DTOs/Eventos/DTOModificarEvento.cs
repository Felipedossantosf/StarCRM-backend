﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Eventos
{
    public class DTOModificarEvento
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool esCarga { get; set; }
    }
}
