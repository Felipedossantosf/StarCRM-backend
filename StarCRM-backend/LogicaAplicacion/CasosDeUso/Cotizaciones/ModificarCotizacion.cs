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
    public class ModificarCotizacion : IModificarCotizacion
    {
        public IRepositorio<Cotizacion> RepoCotizacion { get; set; }
        public IRepositorioLineaCotizacion RepoLineaCotizacion { get; set; }
        public ModificarCotizacion(IRepositorio<Cotizacion> repoCotizacion, IRepositorioLineaCotizacion repoLineaCotizacion)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
        }

        public DTOListarCotizacion Modificar(int id, DTOListarCotizacion dtoCotizacion)
        {
            Cotizacion cotizacionBuscada = RepoCotizacion.FindById(id);
            if (cotizacionBuscada == null)
                throw new KeyNotFoundException($"No se encontró cotización con id: {id}");

            try
            {
                cotizacionBuscada.estado = dtoCotizacion.estado;
                cotizacionBuscada.motivos = dtoCotizacion.motivos;
                cotizacionBuscada.fecha = dtoCotizacion.fecha;
                cotizacionBuscada.metodosPago = dtoCotizacion.metodosPago;
                cotizacionBuscada.notas = dtoCotizacion.notas;
                cotizacionBuscada.subtotal = dtoCotizacion.subtotal;
                cotizacionBuscada.porcDesc = dtoCotizacion.porcDesc;
                cotizacionBuscada.subtotalDesc = dtoCotizacion.subtotalDesc;
                cotizacionBuscada.porcIva = dtoCotizacion.porcIva;
                cotizacionBuscada.total = dtoCotizacion.total;
                cotizacionBuscada.cliente_id = dtoCotizacion.cliente_id;
                cotizacionBuscada.empresa_id = dtoCotizacion.empresa_id;
                cotizacionBuscada.usuario_id = dtoCotizacion.usuario_id;


                RepoCotizacion.Update(id, cotizacionBuscada);
                dtoCotizacion.id = cotizacionBuscada.id;

                // Actualizar las lineas cotización
                ActualizarLineas(id, dtoCotizacion.lineas);

                return dtoCotizacion;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ActualizarLineas(int cotizacion_id, IEnumerable<DTOLineaCotizacion> lineas)
        {
            // Elimnar lineas existentes
            RepoLineaCotizacion.EliminarLineasDeCotizacion(cotizacion_id);
            
            // insertar nuevas lineas
            RepoLineaCotizacion.CrearLineasDeCotizacion(lineas.Select(l => new LineaCotizacion()
            {
                cotizacion_id = cotizacion_id,
                precioUnit = l.precioUnit,
                cant = l.cant,
                totalLinea = l.totalLinea
            }));
        }
    }
}
