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
                throw new Exception(e.InnerException?.Message ?? e.Message);
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            return _db.Usuarios.ToList();
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
        }

        public Usuario ObtenerPorUsername(string username)
        {
            try
            {
                return _db.Usuarios.SingleOrDefault(u => u.Username == username);
            }catch(Exception e)
            {
                throw new UsuarioException($"Error al obtener usuario: {e.Message}");
            }
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

        public void Update(int id, Usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("Usuario recibido por parámetro nulo.");

            var usuarioExistente = _db.Usuarios.FirstOrDefault(u => u.UserId == id);
            if (usuarioExistente == null)
                throw new UsuarioException($"No se encontró ningún usuario con el id: {id}");

            try
            {
                usuarioExistente.Username = user.Username;
                usuarioExistente.Email = user.Email;
                usuarioExistente.Password = user.Password;
                usuarioExistente.Nombre = user.Nombre;
                usuarioExistente.Apellido = user.Apellido;
                usuarioExistente.Rol = user.Rol;
                usuarioExistente.Cargo = user.Cargo;

                _db.SaveChanges();
            }
            catch(UsuarioException e)
            {
                throw new UsuarioException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
