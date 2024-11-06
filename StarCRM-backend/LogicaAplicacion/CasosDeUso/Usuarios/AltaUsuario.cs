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
    public class AltaUsuario : IAltaUsuario
    {
        public IRepositorioUsuario RepoUsuarios { get; set; }

        public AltaUsuario(IRepositorioUsuario repoUsuarios)
        {
            RepoUsuarios = repoUsuarios;
        }

        public DTOUsuarioRegistro Registrar(DTOUsuarioRegistro usuario)
        {
            Usuario nuevoUsuario = new Usuario()
            {   
                Email = usuario.Email,
                Password = usuario.Password,   
                Rol = usuario.Rol,
                FullName = usuario.FullName
            };
            try
            {
                nuevoUsuario.validar();
                RepoUsuarios.Add(nuevoUsuario);
                usuario.Id = nuevoUsuario.UserId;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return usuario;
        }
    }
}
