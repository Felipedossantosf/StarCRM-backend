using AccesoDatos.Interfaces;
using DTOs.Cotizacion;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Cotizaciones
{
    public class ObtenerCotizaciones : IObtenerCotizaciones
    {
        public IRepositorio<Cotizacion> RepoCotizacion { get; set; }
        public IRepositorioLineaCotizacion RepoLineaCotizacion { get; set; }
        public ObtenerCotizaciones(IRepositorio<Cotizacion> repoCotizacion, IRepositorioLineaCotizacion repoLineaCotizacion)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
        }

        public IEnumerable<DTOListarCotizacion> GetAllCotizaciones()
        {
            try
            {
                IEnumerable<Cotizacion> cotizaciones = RepoCotizacion.FindAll();

                IEnumerable<LineaCotizacion> lineas = RepoLineaCotizacion.FindAll();

                IEnumerable<DTOListarCotizacion> dtoCotizaciones = cotizaciones.Select(c => new DTOListarCotizacion()
                {
                    id = c.id,
                    estado = c.estado,
                    motivos = c.motivos,
                    fecha = c.fecha,
                    metodosPago = c.metodosPago,
                    notas = c.notas,
                    subtotal = c.subtotal,
                    porcDesc = c.porcDesc,
                    subtotalDesc = c.porcDesc,
                    porcIva = c.porcIva,
                    total = c.total,
                    cliente_id = c.cliente_id,
                    empresa_id = c.empresa_id,
                    usuario_id = c.usuario_id,

                    lineas = lineas.Where(l => l.cotizacion_id == c.id).Select(l => new DTOLineaCotizacion()
                    {
                        id = l.id,
                        cotizacion_id = l.cotizacion_id,
                        precioUnit = l.precioUnit,
                        cant = l.cant,
                        totalLinea = l.totalLinea,
                    })
                });

                return dtoCotizaciones;            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
