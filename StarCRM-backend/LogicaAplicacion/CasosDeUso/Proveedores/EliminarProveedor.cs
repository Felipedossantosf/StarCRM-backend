using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Proveedor
{
    public class EliminarProveedor : IEliminarProveedor
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public IRepositorio<Actividad> RepoActividad { get; set; }

        public EliminarProveedor(IRepositorioComercial repoComercial, IRepositorio<Actividad> repoActividad) 
        {
            RepoComercial = repoComercial;
            RepoActividad = repoActividad;
        }

        public void Eliminar(int id, int usuario_id) 
        {
            if (id <= 0)
            {
                throw new ComercialException("El ID debe ser mayor a cero");
            }

            LogicaNegocio.Entidades.Proveedor proveedorAEliminar = RepoComercial.FindById(id) as LogicaNegocio.Entidades.Proveedor;
            if (proveedorAEliminar == null)
                throw new ComercialException("Proveedor a eliminar no encontrado.");

            Actividad nuevaActividad = new Actividad()
            {
                fecha = DateTime.UtcNow,
                descripcion = $"Se eliminó el proveedor {proveedorAEliminar.nombre}",
                usuario_id = usuario_id
            };

            try
            {
                RepoComercial.Remove(id);
                RepoActividad.Add(nuevaActividad);
            }
            catch (ComercialException ex)
            {
                throw new ComercialException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new ComercialException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ComercialException(ex.Message);
            }
        }
    }
}
