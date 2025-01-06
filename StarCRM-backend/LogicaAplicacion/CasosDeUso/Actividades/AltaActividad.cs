using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Actividades;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Actividades
{
    public class AltaActividad : IAltaActividad
    {
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public AltaActividad(IRepositorio<Actividad> repoActividad)
        {
            RepoActividad = repoActividad;
        }

        public Actividad Alta(Actividad actividad)
        {
            if (actividad == null)
                throw new ArgumentNullException(nameof(actividad), "Actividad recibida por parámetro no puede ser nula.");

            try
            {
                RepoActividad.Add(actividad);
                return actividad;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
