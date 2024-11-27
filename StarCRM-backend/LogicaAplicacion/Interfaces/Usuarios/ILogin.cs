using DTOs.Usuarios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Usuarios
{
    public interface ILogin
    {
        DTOUsuarioLogin Login(DTOLoginRequest dtoLoginRequest);
    }
}
