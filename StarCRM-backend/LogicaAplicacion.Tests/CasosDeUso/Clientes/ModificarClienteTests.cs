using AccesoDatos.Interfaces;
using DTOs.Clientes;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Tests.CasosDeUso.Clientes
{
    public class ModificarClienteTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly Mock<IRepositorio<Actividad>> _mockRepoActividad;
        private readonly ModificarCliente _modificarCliente;

        public ModificarClienteTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _modificarCliente = new ModificarCliente(_mockRepo.Object, _mockRepoActividad.Object);
        }

        [Fact]
        public void Modificar_DeberiaActualizarCliente_CuandoLosDatosSonValidos()
        {
            // Arrange
            var clienteExistente = new Cliente { id = 1, nombre = "Original" };
            var dtoCliente = new DTOCliente { Nombre = "Actualizado" };

            _mockRepo.Setup(repo => repo.FindById(1)).Returns(clienteExistente);
            _mockRepo.Setup(repo => repo.UpdateCliente(1, It.IsAny<Cliente>()));

            // Act
            var resultado = _modificarCliente.Modificar(1, dtoCliente);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateCliente(1, It.IsAny<Cliente>()), Times.Once);
            Assert.Equal("Actualizado", resultado.Nombre);
        }

        [Fact]
        public void Modificar_DeberiaLanzarException_CuandoClienteNoExiste()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindById(1)).Returns((Cliente)null);

            // Act & Assert
            var ex = Assert.Throws<ClienteException>(() => _modificarCliente.Modificar(1, new DTOCliente()));
            Assert.Equal("No se encontró cliente con el id: 1", ex.Message);
        }
    }
}
