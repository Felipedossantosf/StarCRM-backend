using DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Usuarios
{
    public interface IModificarUsuario
    {
        DTOUsuarioRegistro Modificar(int id, DTOUsuarioRegistro dtoUser);
    }
}
