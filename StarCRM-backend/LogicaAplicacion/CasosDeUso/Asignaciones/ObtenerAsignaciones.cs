using AccesoDatos.Interfaces;
using DTOs.Asignaciones;
using LogicaAplicacion.Interfaces.Asignaciones;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Asignaciones
{
    public class ObtenerAsignaciones : IObtenerAsignaciones
    {
        public IRepositorio<Asignacion> RepoAsignacion { get; set; }
        public ObtenerAsignaciones(IRepositorio<Asignacion> repoAsignacion)
        {
            RepoAsignacion = repoAsignacion;
        }

        public IEnumerable<DTOListarAsignacion> GetAsignaciones()
        {


            IEnumerable<Asignacion> asignaciones = RepoAsignacion.FindAll();
            if (asignaciones == null || !asignaciones.Any())
                throw new KeyNotFoundException("No se encontraron asignaciones.");

            IEnumerable<DTOListarAsignacion> dtoAsignaciones = asignaciones.Select(a => new DTOListarAsignacion()
            {
                id = a.id,
                cliente_id = a.cliente_id,
                admin_id = a.admin_id,
                comun_id = a.comun_id,
                fecha = a.fecha,
                descripcion = a.descripcion,
                estado = a.estado,
            });
            
            return dtoAsignaciones;
        }
    }
}
