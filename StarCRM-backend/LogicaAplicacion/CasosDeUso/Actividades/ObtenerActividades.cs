using AccesoDatos.Interfaces;
using DTOs.Actividades;
using LogicaAplicacion.Interfaces.Actividades;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Actividades
{
    public class ObtenerActividades : IObtenerActividades
    {
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public ObtenerActividades(IRepositorio<Actividad> repo) { 
            RepoActividad = repo;
        }
        public IEnumerable<DTOListarActividad> getAllActividades()
        {
            IEnumerable<Actividad> actividades = RepoActividad.FindAll();
            if (actividades == null | !actividades.Any())
                throw new KeyNotFoundException("No se encontraron actividades en el historial.");

            IEnumerable<DTOListarActividad> dtoActividades = actividades.Select(a => new DTOListarActividad()
            {
                id = a.id,
                fecha = a.fecha,
                descripcion = a.descripcion,
                usuario_id = a.usuario_id
            });

            return dtoActividades;
        }
    }
}
