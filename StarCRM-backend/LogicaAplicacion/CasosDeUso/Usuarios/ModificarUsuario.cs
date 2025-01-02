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
    public class ModificarUsuario : IModificarUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public ModificarUsuario(IRepositorioUsuario repoUsuario)
        {
            RepoUsuario = repoUsuario;
        }

        public DTOUsuarioRegistro Modificar(int id, DTOUsuarioRegistro dtoUser)
        {
            Usuario usuarioBuscado = RepoUsuario.FindById(id);
            if (usuarioBuscado == null)
                throw new ArgumentNullException($"No se encontró el usuario con id: {id}");

            try
            {
                usuarioBuscado.Username = dtoUser.Username;
                usuarioBuscado.Email = dtoUser.Email;
                usuarioBuscado.Password = usuarioBuscado.EncriptarPass(dtoUser.Password);
                usuarioBuscado.Rol = dtoUser.Rol;
                usuarioBuscado.Nombre = dtoUser.Nombre;
                usuarioBuscado.Apellido = dtoUser.Apellido;
                usuarioBuscado.Cargo = dtoUser.Cargo;

                RepoUsuario.Update(id, usuarioBuscado);
                dtoUser.UserId = id;

                return dtoUser;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(UsuarioException e)
            {
                throw new UsuarioException(e.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
