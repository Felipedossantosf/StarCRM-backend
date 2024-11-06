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
    public class Login : ILogin
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public Login(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        DTOUsuarioLogin ILogin.Login(DTOUsuarioLogin usuario)
        {
            Usuario usuarioBuscado = RepoUsuarios.IniciarSesion(usuario.Email, usuario.Password);
            if(usuarioBuscado != null)
            {
                usuario.Id = usuarioBuscado.UserId;
                usuario.FullName = usuarioBuscado.FullName;
            };
            return usuario;
        }
    }
}
