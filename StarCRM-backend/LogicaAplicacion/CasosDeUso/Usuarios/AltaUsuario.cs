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
            Usuario nuevoUsuario = new Usuario()
            {
                Username = usuario.Username,
                Email = usuario.Email,
                // Encrypt de contraseña
                Password = EncriptarPass(usuario.Password),
                Rol = usuario.Rol,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Cargo = usuario.Cargo,
            };
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

        // Encrypt de contraseña
        private string EncriptarPass(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(10));

        }
    }
}
