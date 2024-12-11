using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    
    public class Cliente:Comercial,  IValidable
    {
        
        

        public string paises { get; set; }
        public string tipoMercaderia { get; set; }
        public string frecuencia { get; set; }
        public string mediosTransporte { get; set; }
        public string zafras { get; set; }
        public string notas { get; set; }
        public bool? esInactivo { get; set; }
        public DateTime? fechaUltCarga { get; set; }
        public string estado { get; set; }

        public void validar()
        {
            if (String.IsNullOrEmpty(paises)) throw new ClienteException("paises no puede ser vacío.");
        }
        
    }
}
