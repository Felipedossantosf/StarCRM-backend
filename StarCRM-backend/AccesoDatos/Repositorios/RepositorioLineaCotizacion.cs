using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioLineaCotizacion : IRepositorioLineaCotizacion
    {
        private StarCRMContext _db;
        public RepositorioLineaCotizacion(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(LineaCotizacion obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Linea Cotización recibida por parámetro nula.");

            try
            {
                _db.LineasCotizacion.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CrearLineasDeCotizacion(IEnumerable<LineaCotizacion> lineas)
        {
            if (lineas == null)
                throw new ArgumentNullException("Lineas recibidas por parametros = null");

            try
            {
                _db.LineasCotizacion.AddRange(lineas);
                _db.SaveChanges();  
            }catch(Exception e)
            {
                throw new Exception($"Error en RepositorioLineaCotizacion: {e.InnerException?.Message ?? e.Message}");
            }
        }

        public void EliminarLineasDeCotizacion(int cotizacion_id)
        {
            try
            {
                IEnumerable<LineaCotizacion> lineas = _db.LineasCotizacion.Where(l => l.cotizacion_id == cotizacion_id).ToList();
                _db.LineasCotizacion.RemoveRange(lineas);
            }
            catch(Exception e)
            {
                throw new Exception ($"Error al eliminar lineas en RepositorioLineaCotizacion: {e.InnerException?.Message ?? e.Message}");
            }
        }

        public IEnumerable<LineaCotizacion> FindAll()
        {
            return _db.LineasCotizacion.ToList();
        }

        public LineaCotizacion FindById(int? id)
        {
            if (id == null) throw new ArgumentNullException("id linea cotización recibido por parámetro nulo.");
            try
            {
                return _db.LineasCotizacion.SingleOrDefault(c => c.id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<LineaCotizacion> GetLineasDeCotizacion(int cotizacion_id)
        {
            return _db.LineasCotizacion
                .Include(lc => lc.cotizacion)
                .Where(lc => lc.cotizacion_id == cotizacion_id)
                .ToList();
        }

        public void Remove(LineaCotizacion obj)
        {
            throw new NotImplementedException();            
        }

        public void Remove(int? id)
        {
            if (id <= 0)
                throw new ArgumentNullException("El id recibido por parámetro no es válido.");

            try
            {
                var lineaCotizacion = FindById(id);
                if (lineaCotizacion == null)
                    throw new KeyNotFoundException($"No se encontró la linea cotización con id: {id}");

                _db.LineasCotizacion.Remove(lineaCotizacion);
                _db.SaveChanges();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(LineaCotizacion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, LineaCotizacion obj)
        {
            if (obj == null) throw new ArgumentNullException("LineaCotizacion recibida por parámetros nula.");

            try
            {
                var lineaCotizacion = FindById(id);
                if (lineaCotizacion == null)
                    throw new KeyNotFoundException($"No se encontró LineaCotización con el id: {id}");
                
                lineaCotizacion.precioUnit = obj.precioUnit;
                lineaCotizacion.totalLinea = obj.totalLinea;
                lineaCotizacion.cant = obj.cant;

                _db.SaveChanges();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
