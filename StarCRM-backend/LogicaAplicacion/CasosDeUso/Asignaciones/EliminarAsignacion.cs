using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Asignaciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Asignaciones
{
    public class EliminarAsignacion : IEliminarAsignacion
    {
        public IRepositorio<Asignacion> RepoAsignacion { get; set; }
        public EliminarAsignacion(IRepositorio<Asignacion> repoAsignacion)
        {
            RepoAsignacion = repoAsignacion;
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new AsignacionException("El id debe ser mayor a 0.");

            try
            {
                RepoAsignacion.Remove(id);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch(AsignacionException e)
            {
                throw new AsignacionException(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
