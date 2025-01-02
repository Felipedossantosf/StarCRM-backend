using AccesoDatos.Interfaces;
using DTOs.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
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
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.Username = usuario.Username;
            nuevoUsuario.Email = usuario.Email;
            nuevoUsuario.Password = nuevoUsuario.EncriptarPass(usuario.Password);
            nuevoUsuario.Rol = usuario.Rol;
            nuevoUsuario.Nombre = usuario.Nombre;
            nuevoUsuario.Apellido = usuario.Apellido;
            nuevoUsuario.Cargo = usuario.Cargo;
            try
            {
                nuevoUsuario.validar();
                RepoUsuarios.Add(nuevoUsuario);
                usuario.UserId = nuevoUsuario.UserId;
            }catch(UsuarioException ue)
            {
                throw new UsuarioException(ue.Message);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return usuario;
        }

    }
}
