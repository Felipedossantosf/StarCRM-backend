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

        DTOUsuarioLogin ILogin.Login(DTOLoginRequest dtoLoginRequest)
        {
            Usuario usuarioBuscado = RepoUsuarios.IniciarSesion(dtoLoginRequest.username, dtoLoginRequest.password);
            DTOUsuarioLogin dtoUsuario = new DTOUsuarioLogin();


            if (usuarioBuscado != null)
            {
                dtoUsuario.Id = usuarioBuscado.UserId;
                dtoUsuario.Username = usuarioBuscado.Username;
                dtoUsuario.Password = usuarioBuscado.Password;
            };
            return dtoUsuario;
        }
    }
}
