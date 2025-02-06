using AccesoDatos.Interfaces;
using DTOs.Cotizacion;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public ModificarCotizacion(IRepositorio<Cotizacion> repoCotizacion,
            IRepositorioLineaCotizacion repoLineaCotizacion,
            IRepositorio<Actividad> repoActividad)
        {
            RepoCotizacion = repoCotizacion;
            RepoLineaCotizacion = repoLineaCotizacion;
            RepoActividad = repoActividad;
        }

        public DTOListarCotizacion Modificar(int id, DTOListarCotizacion dtoCotizacion)
        {
            Cotizacion cotizacionBuscada = RepoCotizacion.FindById(id);
            if (cotizacionBuscada == null)
                throw new KeyNotFoundException($"No se encontró cotización con id: {id}");

            try
            {
                cotizacionBuscada.estado = dtoCotizacion.estado;                
                cotizacionBuscada.fecha = dtoCotizacion.fecha;
                cotizacionBuscada.metodosPago = dtoCotizacion.metodosPago;                
                cotizacionBuscada.subtotal = dtoCotizacion.subtotal;
                cotizacionBuscada.porcDesc = dtoCotizacion.porcDesc;
                cotizacionBuscada.subtotalDesc = dtoCotizacion.subtotalDesc;
                cotizacionBuscada.porcIva = dtoCotizacion.porcIva;
                cotizacionBuscada.total = dtoCotizacion.total;
                cotizacionBuscada.cliente_id = dtoCotizacion.cliente_id;
                cotizacionBuscada.empresa_id = dtoCotizacion.empresa_id;
                cotizacionBuscada.usuario_id = dtoCotizacion.usuario_id;
                cotizacionBuscada.proveedor_id = dtoCotizacion.proveedor_id;
                cotizacionBuscada.fechaValidez = dtoCotizacion.fechaValidez;
                cotizacionBuscada.origen = dtoCotizacion.origen;
                cotizacionBuscada.destino = dtoCotizacion.destino;
                cotizacionBuscada.condicionFlete = dtoCotizacion.condicionFlete;
                cotizacionBuscada.modo = dtoCotizacion.modo;
                cotizacionBuscada.tipo = dtoCotizacion.tipo;
                cotizacionBuscada.mercaderia = dtoCotizacion.mercaderia;
                cotizacionBuscada.peso = dtoCotizacion.peso;
                cotizacionBuscada.volumen = dtoCotizacion.volumen;
                cotizacionBuscada.terminosCondiciones = dtoCotizacion.terminosCondiciones;
                cotizacionBuscada.incoterm = dtoCotizacion.incoterm;
                cotizacionBuscada.bulto = dtoCotizacion.bulto;
                cotizacionBuscada.precioMetro = dtoCotizacion.precioMetro;
                cotizacionBuscada.Att = dtoCotizacion.Att;

                RepoCotizacion.Update(id, cotizacionBuscada);
                dtoCotizacion.id = cotizacionBuscada.id;

                // Actualizar las lineas cotización
                ActualizarLineas(id, dtoCotizacion.lineas);

                Actividad nuevaActividad = new Actividad()
                {
                    fecha = DateTime.Now,
                    descripcion = $"Se modificó la cotización #{cotizacionBuscada.id}",
                    usuario_id = dtoCotizacion.usuario_id
                };
                RepoActividad.Add(nuevaActividad);

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
                descripcion = l.descripcion,
                iva = l.iva,
                precioUnit = l.precioUnit,
                cant = l.cant,
                totalLinea = l.totalLinea
            }));
        }
    }
}
