using AccesoDatos.Interfaces;
using DTOs.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Usuarios
{
    public class ObtenerUsuario : IObtenerUsuario
    {
        public IRepositorioUsuario RepositorioUsuario { get; set; }
        public ObtenerUsuario(IRepositorioUsuario repositorioUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
        }

        public DTOUsuarioRegistro FindById(int id)
        {
            Usuario user = RepositorioUsuario.FindById(id);
            DTOUsuarioRegistro dtoUser = new DTOUsuarioRegistro()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Rol = user.Rol,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
            };
            return dtoUser;
        }
    }
}
