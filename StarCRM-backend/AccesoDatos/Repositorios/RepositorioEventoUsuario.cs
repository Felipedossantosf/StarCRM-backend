using AccesoDatos.Interfaces;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class RepositorioEventoUsuario : IRepositorioEventoUsuario
    {
        private StarCRMContext _db;
        public RepositorioEventoUsuario(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(EventoUsuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Objeto recibido por parámetro nulo.");

            try
            {
                _db.EventoUsuarios.Add(obj);
                _db.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CrearEventoUsuarios(IEnumerable<EventoUsuario> eventoUsuarios)
        {
            if (eventoUsuarios == null)
                throw new ArgumentNullException("Lista recibida por parámetro nula.");

            try
            {
                _db.EventoUsuarios.AddRange(eventoUsuarios);
                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void EliminarPorEvento(int eventoId)
        {
            try
            {
                var relaciones = _db.Set<EventoUsuario>().Where(eu => eu.evento_id == eventoId);
                _db.Set<EventoUsuario>().RemoveRange(relaciones);
                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public IEnumerable<EventoUsuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public EventoUsuario FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetUsuarioDeEvento(int eventoId)
        {
            try
            {
                return _db.EventoUsuarios
                            .Where(eu => eu.evento_id == eventoId)
                            .Select(eu => eu.usuario_id);   
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(EventoUsuario obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(EventoUsuario obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EventoUsuario obj)
        {
            throw new NotImplementedException();
        }

    }
}
