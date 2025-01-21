using AccesoDatos.Interfaces;
using DTOs.Cotizacion;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Cotizaciones
{
    public class AltaCotizacion : IAltaCotizacion
    {
        public IRepositorio<Cotizacion> RepoCotizacion { get; set; }
        public IRepositorioLineaCotizacion RepoLineaCotizacion { get; set; }
        public AltaCotizacion(IRepositorio<Cotizacion> repoCotizacion, IRepositorioLineaCotizacion repoLineaCotizacion)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
        }

        public DTOListarCotizacion Alta(DTOListarCotizacion dtoCotizacion)
        {
            if(dtoCotizacion == null)
                throw new ArgumentNullException(nameof(dtoCotizacion));
            try
            {
                Cotizacion newCotizacion = new Cotizacion()
                {
                    estado = dtoCotizacion.estado,
                    motivos = dtoCotizacion.motivos,
                    fecha = dtoCotizacion.fecha,
                    metodosPago = dtoCotizacion.metodosPago,
                    notas = dtoCotizacion.notas,
                    subtotal = dtoCotizacion.subtotal,
                    porcDesc = dtoCotizacion.porcDesc,
                    subtotalDesc = dtoCotizacion.subtotalDesc,
                    porcIva = dtoCotizacion.porcIva,
                    total = dtoCotizacion.total,
                    cliente_id = dtoCotizacion.cliente_id,
                    empresa_id = dtoCotizacion.empresa_id,
                    usuario_id = dtoCotizacion.usuario_id
                };

                RepoCotizacion.Add(newCotizacion);
                dtoCotizacion.id = newCotizacion.id;

                IEnumerable<LineaCotizacion> lineas = dtoCotizacion.lineas.Select(l => new LineaCotizacion()
                {
                    cotizacion_id = newCotizacion.id,
                    cant = l.cant,
                    precioUnit = l.precioUnit,
                    totalLinea = l.totalLinea,
                });

                RepoLineaCotizacion.CrearLineasDeCotizacion(lineas);

                return dtoCotizacion;
            }catch(CotizacionException e)
            {
                throw new CotizacionException(e.Message);
            }catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }
    }
}
