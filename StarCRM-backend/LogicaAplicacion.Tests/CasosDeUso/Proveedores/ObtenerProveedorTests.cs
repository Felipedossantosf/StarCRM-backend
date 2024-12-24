using AccesoDatos.Interfaces;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Proveedor;
using LogicaNegocio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Proveedores
{
    public class ObtenerProveedorTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly ObtenerProveedor _obtenerProveedor;

        public ObtenerProveedorTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _obtenerProveedor = new ObtenerProveedor(_mockRepo.Object);
        }

        [Fact]
        public void ObtenerPorId_DeberiaRetornarCliente_CuandoExiste()
        {
            // Arrange
            var proveedor = new Proveedor { id = 1, nombre = "Proveedor Existente" };
            _mockRepo.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Comercial, bool>>>()))
                     .Returns(new List<Comercial> { proveedor });

            // Act
            var resultado = _obtenerProveedor.ObtenerPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Proveedor Existente", resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_DeberiaLanzarException_CuandoIdEsInvalido()
        {
            // Arrange
            var repoMock = new Mock<IRepositorioComercial>();
            var obtenerProveedor = new ObtenerProveedor(repoMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => obtenerProveedor.ObtenerPorId(0));
        }

        [Fact]
        public void ObtenerPorId_DeberiaLanzarException_CuandoNoSeEncuentraCliente()
        {
            // Arrange
            var repoMock = new Mock<IRepositorioComercial>();
            repoMock.Setup(r => r.FindById(It.IsAny<int>())).Returns((Comercial)null);

            var obtenerProveedor = new ObtenerProveedor(repoMock.Object);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => obtenerProveedor.ObtenerPorId(100));
        }
    }
}
