using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IRepositorioComercial: IRepositorio<Comercial>
    {
        IEnumerable<Comercial> FindByCondition(Expression<Func<Comercial, bool>> expression);
        IEnumerable<Cliente> FindAllClientes();
        IEnumerable<Proveedor> FindAllProveedores();
    }
}
