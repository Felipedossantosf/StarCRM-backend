using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Cotizaciones
{
    public class EliminarCotizacion : IEliminarCotizacion
    {
        public IRepositorio<Cotizacion> RepoCotizacion { get; set; }
        public IRepositorioLineaCotizacion RepoLineaCotizacion { get; set; }
        public EliminarCotizacion(IRepositorio<Cotizacion> repoCotizacion, IRepositorioLineaCotizacion repoLineaCotizacion)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
        }

        public void Eliminar(int cotizacion_id)
        {
            if (cotizacion_id <= 0)
                throw new ArgumentNullException("Id recibido por parámetro inválido.");
            try
            {
                RepoLineaCotizacion.EliminarLineasDeCotizacion(cotizacion_id);
                RepoCotizacion.Remove(cotizacion_id);                
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
