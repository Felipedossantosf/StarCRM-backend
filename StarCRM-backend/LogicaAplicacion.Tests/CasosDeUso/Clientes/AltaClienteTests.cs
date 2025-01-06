using AccesoDatos.Interfaces;
using DTOs.Clientes;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.Interfaces.Clientes;
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
    public class AltaClienteTests
    {
        private readonly Mock<IRepositorioComercial> _mockRepo;
        private readonly Mock<IRepositorio<Actividad>> _mockRepoActividad;
        private readonly IAltaCliente _altaCliente;

        public AltaClienteTests()
        {
            _mockRepo = new Mock<IRepositorioComercial>();
            _altaCliente = new AltaCliente(_mockRepo.Object, _mockRepoActividad.Object);
        }

        //[Fact]
        //public void AltaCliente_DeberiaAgregarCliente_CuandoLosDatosSonValidos()
        //{
        //    // Arrange
        //    var dtoCliente = new DTOCliente
        //    {
        //        Nombre = "Cliente tests",
        //        Telefono = "12345678",
        //        Correo = "cliente@correo.com",
        //        Credito = "1200",
        //        RazonSocial = "123123000",
        //        Direccion = "Calle tests 123",
        //        TipoComercial = "Cliente"
        //    };

        //    // Configuramos el mock para asignar un ID
        //    _mockRepoComercial
        //        .Setup(repo => repo.Add(It.IsAny<Comercial>()))
        //        .Callback<Comercial>(comercial =>
        //        {
        //            if (comercial is Cliente cliente)
        //            {
        //                cliente.id = 1;
        //            }
        //        });

        //    // Act
        //    var resultado = _altaCliente.AltaCliente(dtoCliente);

        //    // Assert
        //    _mockRepoComercial.Verify(repo => repo.Add(It.IsAny<Cliente>()), Times.Once);
        //    Assert.NotNull(resultado);
        //    Assert.Equal(dtoCliente.Nombre, resultado.Nombre);
        //    Assert.NotEqual(0, resultado.Id);
        //}

        //[Fact]
        //public void AltaCliente_DeberiaLanzarExcepcion_CuandoElRepositorioFalla()
        //{
        //    // Arrange
        //    var dtoCliente = new DTOCliente
        //    {
        //        Nombre = "Cliente fallido",
        //        Telefono = "123123123",
        //        Correo = "fallo@correo.com",
        //        TipoComercial = "Cliente"
        //    };

        //    // Configuramos el mock para lanzar una exception
        //    _mockRepoComercial
        //        .Setup(repo => repo.Add(It.IsAny<Cliente>()))
        //        .Throws(new Exception("Error de base de datos"));

        //    // Act & Assert
        //    var exception = Assert.Throws<Exception>(() => _altaCliente.AltaCliente(dtoCliente));
        //    Assert.Equal("Error de base de datos", exception.Message);

        //    // Verificar que se intentó agregar al repositorio
        //    _mockRepoComercial.Verify(repo => repo.Add(It.IsAny<Cliente>()), Times.Once);

        //}

        [Fact]
        public void AltaCliente_DeberiaAgregarCliente_CuandoLosDatosSonValidos()
        {
            // Arrange
            var dtoCliente = new DTOCliente { Nombre = "Cliente Prueba", Correo = "cliente@prueba.com" };
            _mockRepo.Setup(repo => repo.Add(It.IsAny<Cliente>()));

            // Act
            var resultado = _altaCliente.AltaCliente(dtoCliente);

            // Assert
            _mockRepo.Verify(repo => repo.Add(It.IsAny<Cliente>()), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal("Cliente Prueba", resultado.Nombre);
        }

        [Fact]
        public void AltaCliente_DeberiaLanzarException_CuandoDtoClienteEsNulo()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _altaCliente.AltaCliente(null));
        }

        [Fact]
        public void AltaCliente_DeberiaLanzarComercialException_CuandoNombreDuplicado()
        {
            // Arrange
            var dtoCliente = new DTOCliente { Nombre = "Duplicado" };
            _mockRepo.Setup(repo => repo.Add(It.IsAny<Cliente>()))
                     .Throws(new ComercialException("Ya hay un comercial registrado con ese nombre."));

            // Act & Assert
            var ex = Assert.Throws<ComercialException>(() => _altaCliente.AltaCliente(dtoCliente));
            Assert.Equal("Ya hay un comercial registrado con ese nombre.", ex.Message);
        }

    }
}
