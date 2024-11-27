using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        public Usuario IniciarSesion(string email, string password);
        
    }
}
