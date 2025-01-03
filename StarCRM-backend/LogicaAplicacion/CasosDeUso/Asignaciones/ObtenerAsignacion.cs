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
    public class ObtenerAsignacion : IObtenerAsignacion
    {
        public IRepositorio<Asignacion> RepoAsignacion { get; set; }
        public ObtenerAsignacion(IRepositorio<Asignacion> repoAsignacion)
        {
            RepoAsignacion = repoAsignacion;
        }

        public DTOListarAsignacion ObtenerPorId(int id)
        {
            try
            {
                Asignacion asignacion = RepoAsignacion.FindById(id);
                DTOListarAsignacion dtoAsignacion = new DTOListarAsignacion()
                {
                    id = asignacion.id,
                    cliente_id = asignacion.id,
                    admin_id = asignacion.admin_id,
                    comun_id = asignacion.comun_id,
                    fecha = asignacion.fecha,
                    descripcion = asignacion.descripcion,
                    estado = asignacion.estado,
                };
                return dtoAsignacion;
            }catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
