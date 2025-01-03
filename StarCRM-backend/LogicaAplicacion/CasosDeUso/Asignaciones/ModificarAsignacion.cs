using AccesoDatos.Interfaces;
using DTOs.Asignaciones;
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
    public class ModificarAsignacion : IModificarAsignacion
    {
        public IRepositorio<Asignacion> RepoAsignacion { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public ModificarAsignacion(IRepositorio<Asignacion> repoAsignacion, IRepositorioUsuario repoUsuario)
        {
            RepoAsignacion = repoAsignacion;
            RepoUsuario = repoUsuario;
        }

        public DTOModificarAsignacion Modificar(int id, DTOModificarAsignacion dtoModificarAsignacion)
        {
            try
            {
                Asignacion asignacionBuscada = RepoAsignacion.FindById(id);
                if (asignacionBuscada == null)
                    throw new AsignacionException($"No se encontró asignación con id: {id}");

                Usuario userAdmin = RepoUsuario.FindById(dtoModificarAsignacion.admin_id);
                if (userAdmin.Rol.ToLower() != "admin")
                    throw new AsignacionException("usuario no habilitado para realizar esta acción.");

                asignacionBuscada.comun_id = dtoModificarAsignacion.comun_id;
                asignacionBuscada.admin_id = dtoModificarAsignacion.admin_id;
                asignacionBuscada.estado = dtoModificarAsignacion.estado;

                RepoAsignacion.Update(id, asignacionBuscada);
                
                
                return dtoModificarAsignacion;
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
