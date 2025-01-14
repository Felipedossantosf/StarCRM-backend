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
            throw new NotImplementedException();
        }

        public void Update(Evento obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Evento obj)
        {
            throw new NotImplementedException();
        }
    }
}
