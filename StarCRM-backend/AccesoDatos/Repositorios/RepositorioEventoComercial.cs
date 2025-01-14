using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioEventoComercial : IRepositorioEventoComercial
    {
        private StarCRMContext _db;
        public RepositorioEventoComercial(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(EventoComercial obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Objeto recibido por parámetro nulo.");

            try
            {
                _db.EventoComerciales.Add(obj);
                _db.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CrearEventoComerciales(IEnumerable<EventoComercial> events)
        {
            if (events == null) throw new ArgumentNullException("Lista recibida por parámetro nula.");
            try
            {
                _db.EventoComerciales.AddRange(events);
                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EventoComercial> FindAll()
        {
            throw new NotImplementedException();
        }

        public EventoComercial FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(EventoComercial obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(EventoComercial obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EventoComercial obj)
        {
            throw new NotImplementedException();
        }
    }
}
