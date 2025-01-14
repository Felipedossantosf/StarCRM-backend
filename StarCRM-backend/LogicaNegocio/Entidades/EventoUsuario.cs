using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    [Table("Evento_Usuario")]
    public class EventoUsuario
    {
        public int evento_id { get; set; }
        public Evento evento { get; set; }
        public int usuario_id { get; set; }
    }
}
