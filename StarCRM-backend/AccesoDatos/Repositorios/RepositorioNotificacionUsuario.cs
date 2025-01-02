using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioNotificacionUsuario : IRepositorioNotificacionUsuario
    {
        private StarCRMContext _db;
        public RepositorioNotificacionUsuario(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(NotificacionUsuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException("No se recibio el objeto NotificacionUsuario.");

            try
            {
                obj.validar();
                _db.NotificacionesUsuario.Add(obj);
                _db.SaveChanges();
            }
            catch(NotificacionUsuarioException e)
            {
                throw new NotificacionUsuarioException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CrearNotificacionesUsuario(IEnumerable<NotificacionUsuario> notificacionesUsuario)
        {
            if (notificacionesUsuario == null)
                throw new ArgumentNullException("No se recibió parametro válido.");

            try
            {
                _db.NotificacionesUsuario.AddRange(notificacionesUsuario);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Error al guardar en la base de datos en RepositorioNotificacionUsuario: {e.InnerException?.Message ?? e.Message}", e);
            }
        }

        public IEnumerable<NotificacionUsuario> FindAll()
        {
            return _db.NotificacionesUsuario.ToList();
        }

        public NotificacionUsuario FindById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Id NotificacionUsuario nulo.");

            return _db.NotificacionesUsuario.SingleOrDefault(obj => obj.id == id);
        }

        public IEnumerable<NotificacionUsuario> ObtenerNotificacionesPorUsuario(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("El id del usuario debe ser mayor a 0.");

            return _db.NotificacionesUsuario
                .Include(nu => nu.notificacion)
                .Where(nu => nu.usuario_id == userId)
                .ToList();
        }

        public void Remove(NotificacionUsuario obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(NotificacionUsuario obj)
        {
            throw new NotImplementedException();
        }

        // Sacar notificación de la lista 
        public void Update(int id, NotificacionUsuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException("NotificacionUsuario recibida por parámetro null.");

            var notificacionExistente = _db.NotificacionesUsuario.FirstOrDefault(obj => obj.id == id);

            if (notificacionExistente == null)
                throw new NotificacionUsuarioException("No se encontró notificacion con el id ingresado.");

            try
            {
                // El único cambio que puede haber (si se lista o no)
                notificacionExistente.activa = obj.activa;

                _db.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
