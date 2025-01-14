using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Eventos
{
    public class DTOListarEvento
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string nombre { get; set; }
        public string descrpicion { get; set; }
        public bool esCarga { get; set; }
        public IEnumerable<int> usuariosId { get; set; }
        public IEnumerable<int> comercialesId { get; set; }

    }
}
