using AccesoDatos.Interfaces;
using DTOs.Clientes;
using DTOs.Proveedor;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Proveedor;
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
    public class ModificarProveedorTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly ModificarProveedor _modificarProveedor;

        public ModificarProveedorTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _modificarProveedor = new ModificarProveedor(_mockRepo.Object);
        }

        [Fact]
        public void Modificar_DeberiaActualizarProveedor_CuandoLosDatosSonValidos()
        {
            // Arrange
            var proveedorExistente = new Proveedor { id = 1, nombre = "Original" };
            var dtoProveedor = new DTOProveedor { Nombre = "Actualizado" };

            _mockRepo.Setup(repo => repo.FindById(1)).Returns(proveedorExistente);
            _mockRepo.Setup(repo => repo.Update(1, It.IsAny<Comercial>()));

            // Act
            var resultado = _modificarProveedor.Modificar(1, dtoProveedor);

            // Assert
            _mockRepo.Verify(repo => repo.Update(1, It.IsAny<Comercial>()), Times.Once);
            Assert.Equal("Actualizado", resultado.Nombre);
        }

        [Fact]
        public void Modificar_DeberiaLanzarException_CuandoClienteNoExiste()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindById(1)).Returns((Proveedor)null);

            // Act & Assert
            var ex = Assert.Throws<ProveedorException>(() => _modificarProveedor.Modificar(1, new DTOProveedor()));
            Assert.Equal("No se encontró Proveedor con el id: 1", ex.Message);
        }
    }
}
