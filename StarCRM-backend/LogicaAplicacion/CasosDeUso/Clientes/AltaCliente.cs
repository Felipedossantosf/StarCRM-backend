﻿using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
using DTOs.Clientes;
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
    public class AltaCliente : IAltaCliente
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public IRepositorio<Actividad> RepoActividad { get; set; }
        public AltaCliente
        (
            IRepositorioComercial repoComercial,
            IRepositorio<Actividad> repoActividad
        )
        {
            RepoComercial = repoComercial;
            RepoActividad = repoActividad;
        }

        DTOCliente IAltaCliente.AltaCliente(DTOCliente dtoCliente)
        {
            if (dtoCliente == null)
            {
                throw new ArgumentNullException(nameof(dtoCliente), "El DTO Cliente no puede ser nulo");
            }

            Cliente nuevoCliente = new Cliente()
            {
                nombre = dtoCliente.Nombre,
                telefono = dtoCliente.Telefono,
                correo = dtoCliente.Correo,
                credito = dtoCliente.Credito,
                razonSocial = dtoCliente.RazonSocial,
                rut = dtoCliente.Rut,
                direccion = dtoCliente.Direccion,
                sitioWeb = dtoCliente.SitioWeb,
                TipoComercial = dtoCliente.TipoComercial,
                zafras = dtoCliente.Zafras,
                notas = dtoCliente.Notas,
                esInactivo = dtoCliente.EsInactivo,
                fechaUltCarga = dtoCliente.FechaUltCarga,
                estado = dtoCliente.Estado,
            };

            Actividad nuevaActividad = new Actividad()
            {
                fecha = DateTime.UtcNow,
                descripcion = $"Nuevo cliente dado de alta: {nuevoCliente.nombre}",
                usuario_id = 58
            };

            try
            {
                RepoComercial.Add(nuevoCliente);
                RepoActividad.Add(nuevaActividad);
                dtoCliente.Id = nuevoCliente.id;
            }
            catch(ComercialException comExc)
            {
                throw new ComercialException(comExc.Message);
            }
            catch(ClienteException cliExc)
            {
                throw new ClienteException(cliExc.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtoCliente;
        }
    }
}
