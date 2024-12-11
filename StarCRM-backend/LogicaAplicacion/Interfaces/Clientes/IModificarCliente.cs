using DTOs.Clientes;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Interfaces.Clientes
{
    public interface IModificarCliente
    {
        DTOCliente Modificar(int id, DTOCliente dtoCliente);
    }
}
