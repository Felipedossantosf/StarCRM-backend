using AccesoDatos.Interfaces;
using DTOs.Clientes;
using DTOs.Proveedor;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Proveedor;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Proveedores
{
    public class AltaProveedorTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly IAltaProveedor _altaProveedor;

        public AltaProveedorTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _altaProveedor = new AltaProveedor(_mockRepo.Object);
        }

        [Fact]
        public void AltaProveedor_DeberiaAgregarProveedor_CuandoLosDatosSonValidos()
        {
            // Arrange
            var dtoProveedor = new DTOProveedor { Nombre = "Proveedor Prueba", Correo = "proveedor@prueba.com" };            

            
            _altaProveedor.AltaProveedor(dtoProveedor);

            // Assert
            _mockRepo.Verify(repo => repo.Add(It.IsAny<Comercial>()), Times.Once);
        }

        [Fact]
        public void AltaProveedor_DeberiaLanzarException_CuandoDtoProveedorEsNulo()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _altaProveedor.AltaProveedor(null));
        }

        [Fact]
        public void AltaProveedor_DeberiaLanzarComercialException_CuandoNombreDuplicado()
        {
            // Arrange
            var dtoProveedor = new DTOProveedor { Nombre = "Duplicado" };
            _mockRepo.Setup(repo => repo.Add(It.IsAny<Comercial>()))
                     .Throws(new ComercialException("Ya hay un comercial registrado con ese nombre."));

            // Act & Assert
            var ex = Assert.Throws<ComercialException>(() => _altaProveedor.AltaProveedor(dtoProveedor));
            Assert.Equal("Ya hay un comercial registrado con ese nombre.", ex.Message);
        }
    }
}
