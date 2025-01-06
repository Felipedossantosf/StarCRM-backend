using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioActividad : IRepositorio<Actividad>
    {
        private StarCRMContext _db;
        public RepositorioActividad(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Actividad obj)
        {
            if (obj == null) 
                throw new ArgumentNullException("No se recibió actividad por parámetro.");

            try
            {
                _db.Actividades.Add(obj);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Actividad> FindAll()
        {
            return _db.Actividades.ToList();
        }

        public Actividad FindById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Id ingresado no puede ser nulo.");

            return _db.Actividades.SingleOrDefault(a => a.id == id);
        }

        public void Remove(Actividad obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Actividad obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Actividad obj)
        {
            throw new NotImplementedException();
        }
    }
}
