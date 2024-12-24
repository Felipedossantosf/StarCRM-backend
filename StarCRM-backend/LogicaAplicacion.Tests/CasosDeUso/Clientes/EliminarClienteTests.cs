using AccesoDatos.Interfaces;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaNegocio.Excepciones;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Clientes
{
    public class EliminarClienteTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly EliminarCliente _eliminarCliente;

        public EliminarClienteTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _eliminarCliente = new EliminarCliente(_mockRepo.Object);
        }

        [Fact]
        public void Eliminar_DeberiaEliminarCliente_CuandoIdEsValido()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Remove(1));

            // Act
            _eliminarCliente.Eliminar(1);

            // Assert
            _mockRepo.Verify(repo => repo.Remove(1), Times.Once);
        }

        [Fact]
        public void Eliminar_DeberiaLanzarException_CuandoIdEsInvalido()
        {
            // Act & Assert
            Assert.Throws<ComercialException>(() => _eliminarCliente.Eliminar(0));
        }
    }
}
