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
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private StarCRMContext _db;

        public RepositorioUsuario(StarCRMContext db)
        {
            _db = db;
        }

        public void Add(Usuario u)
        {
            if (u == null) throw new UsuarioException("No se recibió el usuario.");
            if (_db.Usuarios.Where(us => us.Username == u.Username).Any()) throw new UsuarioException("Username ya registrado.");
            if (_db.Usuarios.Where(us => us.Email == u.Email).Any()) throw new UsuarioException("Email ya registrado.");

            try
            {
                u.validar();
                _db.Usuarios.Add(u);
                _db.SaveChanges();
            }catch (UsuarioException ex)
            {
                throw new UsuarioException(ex.Message);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int? id)
        {
            try
            {
                if (id == null) throw new UsuarioException("Id usario no puede ser nulo");
                return _db.Usuarios.SingleOrDefault(u => u.UserId == id);                            
            }catch(Exception ex)
            {
                throw new UsuarioException($"Error: {ex.Message}");
            }
        }

        public Usuario IniciarSesion(string username, string password)
        {
            try
            {
                return _db.Usuarios.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            }catch(Exception e)
            {
                throw new UsuarioException($"Error: {e.Message}");
            }

            /*
             * var logueado = 
             * if(logueado != null)
            {
                return logueado;
            }
            return null;*/
        }

        public void Remove(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
