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
    public class RepositorioNotificacion : IRepositorio<Notificacion>
    {

        private StarCRMContext _db;

        public RepositorioNotificacion(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Notificacion notificacion)
        {
            if (notificacion == null) throw new ArgumentNullException("No se recibió notificacion.");

            try
            {
                //notificacion.validar();
                _db.Notificaciones.Add(notificacion);
                _db.SaveChanges();
            }
            catch(NotificacionException e)
            {
                throw new NotificacionException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Error al guardar en la base de datos en RepositorioNotificacion: {e.InnerException?.Message ?? e.Message}", e);
            }
        }

        public IEnumerable<Notificacion> FindAll()
        {
            return _db.Notificaciones.ToList();
        }

        public Notificacion FindById(int? id)
        {
            if(id == null)
                throw new ArgumentNullException("Id notificacion nulo.");
            return _db.Notificaciones.SingleOrDefault(n => n.id == id);
        }

        public void Remove(Notificacion obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Notificacion obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Notificacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
