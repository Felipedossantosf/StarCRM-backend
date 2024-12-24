using AccesoDatos.Interfaces;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaNegocio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Clientes
{
    public class ObtenerClienteTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly ObtenerCliente _obtenerCliente;

        public ObtenerClienteTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _obtenerCliente = new ObtenerCliente(_mockRepo.Object);
        }

        [Fact]
        public void ObtenerPorId_DeberiaRetornarCliente_CuandoExiste()
        {
            // Arrange
            var cliente = new Cliente { id = 1, nombre = "Cliente Existente" };
            _mockRepo.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Comercial, bool>>>()))
                     .Returns(new List<Cliente> { cliente });

            // Act
            var resultado = _obtenerCliente.ObtenerPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Cliente Existente", resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_DeberiaLanzarException_CuandoIdEsInvalido()
        {
            // Arrange
            var repoMock = new Mock<IRepositorioComercial>();
            var obtenerCliente = new ObtenerCliente(repoMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => obtenerCliente.ObtenerPorId(0));
        }

        [Fact]
        public void ObtenerPorId_DeberiaLanzarException_CuandoNoSeEncuentraCliente()
        {
            // Arrange
            var repoMock = new Mock<IRepositorioComercial>();
            repoMock.Setup(r => r.FindById(It.IsAny<int>())).Returns((Cliente)null);

            var obtenerCliente = new ObtenerCliente(repoMock.Object);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => obtenerCliente.ObtenerPorId(100));
        }
    }
}
