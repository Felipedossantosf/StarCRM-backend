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
    public class RepositorioAsignacion : IRepositorio<Asignacion>
    {

        private StarCRMContext _db;
        public RepositorioAsignacion(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Asignacion obj)
        {
            if (obj == null)
                throw new ArgumentNullException("No se recibió asignacion por parámetro.");
            try
            {
                obj.validar();
                _db.Asignaciones.Add(obj);
                _db.SaveChanges();
            }
            catch(AsignacionException e)
            {
                throw new AsignacionException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Error al guardar en la base de datos en RepositorioAsignacion: {e.InnerException?.Message ?? e.Message}", e);
            }
        }

        public IEnumerable<Asignacion> FindAll()
        {
            return _db.Asignaciones.ToList();
        }

        public Asignacion FindById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException("Id asignación no puede ser nulo.");

                return _db.Asignaciones.SingleOrDefault(a => a.id == id);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(Asignacion obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            if (id <= 0)
                throw new AsignacionException("El id proporcionado no es válido.");

            try
            {
                var asignacion = FindById(id);
                if (asignacion == null)
                    throw new AsignacionException($"No se encontró asignación con el id: {id}");

                _db.Asignaciones.Remove(asignacion);
                _db.SaveChanges();
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Asignacion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Asignacion obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Asignacion ingresada por parámetro nula.");

            var asignacionExistente = _db.Asignaciones.FirstOrDefault(a => a.id == id);
            if (asignacionExistente == null)
                throw new AsignacionException($"No se encontró asignacion con id: {id}");

            try
            {
                asignacionExistente.comun_id = obj.comun_id;
                asignacionExistente.admin_id = obj.admin_id;
                asignacionExistente.estado = obj.estado;

                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
