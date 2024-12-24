using AccesoDatos.Interfaces;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Proveedor;
using LogicaNegocio.Excepciones;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Proveedores
{
    public class EliminarProveedorTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly EliminarProveedor _eliminarProveedor;

        public EliminarProveedorTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _eliminarProveedor = new EliminarProveedor(_mockRepo.Object);
        }

        [Fact]
        public void Eliminar_DeberiaEliminarCliente_CuandoIdEsValido()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Remove(1));

            // Act
            _eliminarProveedor.Eliminar(1);

            // Assert
            _mockRepo.Verify(repo => repo.Remove(1), Times.Once);
        }

        [Fact]
        public void Eliminar_DeberiaLanzarException_CuandoIdEsInvalido()
        {
            // Act & Assert
            Assert.Throws<ComercialException>(() => _eliminarProveedor.Eliminar(0));
        }
    }
}
