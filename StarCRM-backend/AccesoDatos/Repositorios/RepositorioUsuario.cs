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
                     
            try
            {
                u.validar();
                _db.Usuarios.Add(u);
                _db.SaveChanges();
            }catch (Exception ex)
            {
                throw new UsuarioException(ex.Message);
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public Usuario IniciarSesion(string email, string password)
        {
            var logueado = _db.Usuarios.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            if(logueado != null)
            {
                return logueado;
            }
            return null;
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
