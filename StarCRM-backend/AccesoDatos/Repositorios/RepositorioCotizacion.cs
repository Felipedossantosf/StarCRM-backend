using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioCotizacion : IRepositorio<Cotizacion>
    {
        private StarCRMContext _db;
        public RepositorioCotizacion(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Cotizacion obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Cotización recibida por parámetro nula.");

            try
            {
                obj.validar();
                _db.Cotizaciones.Add(obj);
                _db.SaveChanges();
            }
            catch(CotizacionException e)
            {
                throw new CotizacionException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception($"Error en RepositorioCotizacion: {e.InnerException?.Message ?? e.Message}");
            }
        }

        public IEnumerable<Cotizacion> FindAll()
        {
            return _db.Cotizaciones.ToList();
        }

        public Cotizacion FindById(int? id)
        {
            if (id == null) throw new ArgumentNullException("id cotización recibido por parámetro nulo.");
            try
            {
                return _db.Cotizaciones.SingleOrDefault(c => c.id == id);
            }catch(Exception e)
            {
                throw new Exception(e.Message);                
            }
        }

        public void Remove(Cotizacion obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            if (id <= 0)
                throw new ArgumentNullException("El id recibido por parámetro no es válido.");

            try
            {
                var cotizacion = FindById(id);
                if (cotizacion == null)
                    throw new KeyNotFoundException($"No se encontró cotización con id: {id}");

                _db.Cotizaciones.Remove(cotizacion);
                _db.SaveChanges();
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception($"Error al eliminar cotización en RepositorioCotizacion: {e.InnerException?.Message ?? e.Message}");
            }
        }

        public void Update(Cotizacion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Cotizacion obj)
        {
            if (obj == null) throw new ArgumentNullException("Cotizacion recibida por parámetros nula.");

            try
            {
                var cotizacion = FindById(id);
                if (cotizacion == null)
                    throw new KeyNotFoundException($"No se encontró cotización con el id: {id}");

                cotizacion.estado = obj.estado;
                cotizacion.motivos = obj.motivos;
                cotizacion.fecha = obj.fecha;
                cotizacion.metodosPago = obj.metodosPago;
                cotizacion.notas = obj.notas;
                cotizacion.subtotal = obj.subtotal;
                cotizacion.porcDesc = obj.porcDesc;
                cotizacion.subtotalDesc = obj.subtotalDesc;
                cotizacion.porcIva = obj.porcIva;
                cotizacion.total = obj.total;
                cotizacion.cliente_id = obj.cliente_id;
                cotizacion.empresa_id = obj.empresa_id;
                cotizacion.usuario_id = obj.usuario_id;

                _db.SaveChanges();
            }
            catch(ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
