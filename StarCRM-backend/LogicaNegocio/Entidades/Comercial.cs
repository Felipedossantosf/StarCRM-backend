using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    [Table("Comercial")]
    public class Comercial: IValidable
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string credito { get; set; }
        public string razonSocial { get; set; }
        public string rut { get; set; }
        public string direccion { get; set; }
        public string sitioWeb { get; set; }
        public string TipoComercial { get; set; }

        public void validar()
        {
            if(String.IsNullOrEmpty(nombre)) { throw new ArgumentNullException("nombre no puede ser nulo."); }
        }
    }
}
