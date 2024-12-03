using AccesoDatos.Interfaces;
using DTOs.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
using LogicaNegocio.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Usuarios
{
    public class ObtenerUsuarios: IObtenerUsuarios
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public ObtenerUsuarios(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        IEnumerable<DTOUsuarioRegistro> IObtenerUsuarios.ObtenerUsuarios()
        {
            IEnumerable<Usuario> usuarios = RepoUsuarios.FindAll();
            IEnumerable<DTOUsuarioRegistro> dtoUsuarios = usuarios.Select(u => new DTOUsuarioRegistro()
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,
                Rol = u.Rol,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Cargo = u.Cargo
            });
            return dtoUsuarios;
        }        
    }
}
