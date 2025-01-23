﻿using AccesoDatos.Interfaces;
using DTOs.Clientes;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.Clientes
{
    public class ObtenerClientesPerdidos : IObtenerClientesPerdidos
    {
        public IRepositorioComercial RepoComercial { get; set; }
        public ObtenerClientesPerdidos(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        public IEnumerable<DTOCliente> GetClientesPerdidos()
        {
            try
            {
                IEnumerable<Cliente> clientesPerdidos = RepoComercial.GetClientesPerdidos();
                IEnumerable<DTOCliente> dtoClientes = clientesPerdidos.Select(c => new DTOCliente()
                {
                    Id = c.id,
                    Nombre = c.nombre,
                    Telefono = c.telefono,
                    Correo = c.correo,
                    Credito = c.credito,
                    RazonSocial = c.razonSocial,
                    Rut = c.rut,
                    Direccion = c.direccion,   
                    SitioWeb = c.sitioWeb,
                    TipoComercial = c.TipoComercial,
                    Zafras = c.zafras,
                    Notas = c.notas,
                    FechaUltCarga = c.fechaUltCarga,
                    EsInactivo = c.esInactivo,
                    Estado = c.estado
                });
                return dtoClientes;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
