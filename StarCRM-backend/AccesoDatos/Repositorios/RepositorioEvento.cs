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
    public class RepositorioEvento : IRepositorio<Evento>
    {
        private StarCRMContext _db;
        public RepositorioEvento(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Evento ev)
        {
            if (ev == null)
                throw new ArgumentNullException("Evento recibido nulo.");

            try
            {
                ev.validar();
                _db.Eventos.Add(ev);
                _db.SaveChanges();
            }catch(EventoException e)
            {
                throw new EventoException(e.Message);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Evento> FindAll()
        {
            return _db.Eventos.ToList();
        }

        public Evento FindById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Id evento no puede ser nulo.");
            try
            {
                return _db.Eventos.SingleOrDefault(e => e.id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(Evento obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            if (id <= 0)
                throw new ArgumentNullException("El id recibido por parámetro no es válido.");

            try
            {
                var evento = FindById(id);
                if (evento == null)
                    throw new KeyNotFoundException($"No se encontró evento con el id: {id}");

                _db.Eventos.Remove(evento);
                _db.SaveChanges();
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception e)
            {
                //throw new Exception(e.Message);
                throw new Exception($"Error al eliminar en la base de datos en RepositorioEvento: {e.InnerException?.Message ?? e.Message}", e);
            }
        }

        public void Update(Evento obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Evento obj)
        {
            if (obj == null) throw new ArgumentNullException("Objeto recibido por parámetro nulo.");

            var evento = _db.Eventos.FirstOrDefault(e => e.id == id);
            if (evento == null)
                throw new EventoException($"No se encontró evento con el id: {id}");

            try
            {
                evento.fecha = obj.fecha;
                evento.nombre = obj.nombre;
                evento.descripcion = obj.descripcion;
                evento.esCarga = obj.esCarga;

                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
