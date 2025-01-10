using AccesoDatos.Interfaces;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Clientes
{
    public class EliminarCliente : IEliminarCliente
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public EliminarCliente(IRepositorioComercial repoComercial, IRepositorio<Actividad> repoActividad)
        {
            RepoComercial = repoComercial;
            RepoActividad = repoActividad;
        }

        public void Eliminar(int id, int usuario_id)
        {
            if(id <= 0)
            {
                throw new ComercialException("El ID debe ser mayor a cero");
            }

            Cliente clienteAEliminar = RepoComercial.FindById(id) as Cliente;
            if (clienteAEliminar == null)
                throw new ComercialException("No se encontró el cliente a eliminar.");

            Actividad nuevaActividad = new Actividad()
            {
                fecha = DateTime.UtcNow,
                descripcion = $"Se eliminó un cliente {clienteAEliminar.nombre}",
                usuario_id = usuario_id
            };

            try
            {
                RepoComercial.Remove(id);
                RepoActividad.Add(nuevaActividad);
            }catch(ComercialException ex)
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
