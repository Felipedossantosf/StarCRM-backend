using AccesoDatos.Interfaces;
using DTOs.Actividades;
using LogicaAplicacion.Interfaces.Actividades;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Actividades
{
    public class ObtenerActividad : IObtenerActividad
    {
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public ObtenerActividad(IRepositorio<Actividad> repoActividad)
        {
            RepoActividad = repoActividad;
        }

        public DTOListarActividad GetPorId(int id)
        {

            try
            {
                Actividad actividad = RepoActividad.FindById(id);
                DTOListarActividad dtoActividad = new DTOListarActividad()
                {
                    id = actividad.id,
                    fecha = actividad.fecha,
                    descripcion = actividad.descripcion,
                    usuario_id = actividad.usuario_id
                };
                return dtoActividad;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
