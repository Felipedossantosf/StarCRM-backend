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
    public class AltaAsignacion : IAltaAsignacion
    {
        public IRepositorio<Asignacion> RepoAsignacion { get; set; }  
        public IRepositorioComercial RepoComercial { get; set; }
        public AltaAsignacion(IRepositorio<Asignacion> repoAsignacion, IRepositorioComercial repoComercial)
        {
            RepoAsignacion = repoAsignacion;
            RepoComercial = repoComercial;
        }

        public DTOAsignacion Alta(DTOAsignacion dtoAsignacion)
        {
            if (dtoAsignacion == null)
                throw new ArgumentNullException(nameof(dtoAsignacion), "El DTO Asignacion no puede ser nulo.");

            Cliente cliente = RepoComercial.FindById(dtoAsignacion.cliente_id) as Cliente;

            if (cliente == null)
            {
                throw new KeyNotFoundException($"No se encontró un cliente con el ID {dtoAsignacion.cliente_id}");
            }


            Asignacion nuevaAsignacion = new Asignacion()
            {
                cliente_id = dtoAsignacion.cliente_id,
                //admin_id = dtoAsignacion.admin_id,
                comun_id = dtoAsignacion.comun_id,
                fecha = dtoAsignacion.fecha,
                descripcion = dtoAsignacion.descripcion,
                estado = dtoAsignacion.estado
            };

            try
            {                

                RepoAsignacion.Add(nuevaAsignacion);

                if (dtoAsignacion.estado.ToLower() == "aprobada")
                {
                    cliente.estado = "Asignado";
                }
                RepoComercial.Update(dtoAsignacion.cliente_id, cliente);

                dtoAsignacion.id = nuevaAsignacion.id;
                return dtoAsignacion;
            }
            catch(AsignacionException e)
            {
                throw new AsignacionException(e.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
