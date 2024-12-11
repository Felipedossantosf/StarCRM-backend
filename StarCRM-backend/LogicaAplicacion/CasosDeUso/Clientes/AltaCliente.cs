using AccesoDatos.Interfaces;
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

        public AltaCliente(IRepositorioComercial repoComercial)
        {
            RepoComercial = repoComercial;
        }

        DTOCliente IAltaCliente.AltaCliente(DTOCliente dtoCliente)
        {
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
                paises = dtoCliente.Paises,
                tipoMercaderia = dtoCliente.TipoMercaderia,
                frecuencia = dtoCliente.Frecuencia,
                mediosTransporte = dtoCliente.MediosTransporte,
                zafras = dtoCliente.Zafras,
                notas = dtoCliente.Notas,
                esInactivo = dtoCliente.EsInactivo,
                fechaUltCarga = dtoCliente.FechaUltCarga,
                estado = dtoCliente.Estado,
            };

            try
            {
                RepoComercial.Add(nuevoCliente);
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
