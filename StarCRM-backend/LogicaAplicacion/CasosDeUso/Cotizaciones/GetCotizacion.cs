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
    public class GetCotizacion : IGetCotizacion
    {
        public IRepositorio<Cotizacion> RepoCotizacion { get; set; }
        public IRepositorioLineaCotizacion RepoLineaCotizacion { get; set; }
        public GetCotizacion(IRepositorio<Cotizacion> repoCotizacion, IRepositorioLineaCotizacion repoLineaCotizacion)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
        }

        public DTOListarCotizacion GetCotizacionPorId(int id)
        {
            try
            {
                Cotizacion cotizacionBuscada = RepoCotizacion.FindById(id);
                if (cotizacionBuscada == null)
                    throw new KeyNotFoundException($"Cotización con id: {id} no encontrada.");

                IEnumerable<LineaCotizacion> lineas = RepoLineaCotizacion.GetLineasDeCotizacion(id);

                IEnumerable<DTOLineaCotizacion> dtoLineas = lineas.Select(l => new DTOLineaCotizacion()
                {
                    id = l.id,
                    cotizacion_id = l.cotizacion_id,
                    cant = l.cant,
                    precioUnit = l.precioUnit,
                    totalLinea = l.totalLinea
                });

                DTOListarCotizacion dtoCotizacion = new DTOListarCotizacion()
                {
                    id = cotizacionBuscada.id,
                    estado = cotizacionBuscada.estado,
                    motivos = cotizacionBuscada.motivos,
                    fecha = cotizacionBuscada.fecha,
                    metodosPago = cotizacionBuscada.metodosPago,
                    notas = cotizacionBuscada.notas,
                    subtotal = cotizacionBuscada.subtotal,
                    porcDesc = cotizacionBuscada.porcDesc,
                    subtotalDesc = cotizacionBuscada.subtotalDesc,
                    porcIva = cotizacionBuscada.porcIva,
                    total = cotizacionBuscada.total,
                    cliente_id = cotizacionBuscada.cliente_id,
                    usuario_id = cotizacionBuscada.usuario_id,
                    empresa_id = cotizacionBuscada.empresa_id,

                    lineas = dtoLineas
                };

                return dtoCotizacion;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
