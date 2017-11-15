using Application.Base;
using Application.Implements;
using Application.Test.Base;
using Domain.Abstracts;
using Domain.Entities;
using Infraestructura.Data.Base;
using Infraestructure.Data.Repositories;
using Moq;
using NUnit.Framework;
using SirccELC.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    [TestFixture]
    public class OperacionesFinancierasServiceTest
    {
        

        IUnitOfWork _unitOfWork;
        ICuentaBancariaRepository _repositoryCuentaBancaria;
        
        Mock<IDbContext> _mockContext;
        Mock<DbSet<CuentaBancaria>> _mockSetCuentaBancaria;

        List<CuentaBancaria> _listCuentaBancaria;
        
        [SetUp]
        public void Initialize()
        {
            _mockContext = new Mock<IDbContext>();
            _unitOfWork = new UnitOfWork(_mockContext.Object);

            _mockSetCuentaBancaria = new Mock<DbSet<CuentaBancaria>>();
            _listCuentaBancaria = new List<CuentaBancaria>() {
               new CuentaAhorro() { Id = 1, Numero="524255",Nombre="Cuenta 1"},
               new CuentaCorriente() { Id = 2, Numero="524256",Nombre="Cuenta 2"},
            };
            _mockSetCuentaBancaria.SetSource(_listCuentaBancaria);
            _mockContext.Setup(c => c.Set<CuentaBancaria>()).Returns(_mockSetCuentaBancaria.Object);

            _repositoryCuentaBancaria = new CuentaBancariaRepository(_mockContext.Object);
        }

        [Test]
        public void TestConsignarAhorro()
        {
            //Arrange
            ConsignarService _service = new ConsignarService(_unitOfWork, _repositoryCuentaBancaria);
            var request = new ConsignarRequest() { NumeroCuenta = "524255", Valor = 1000 };
            //Act
            ConsignarResponse response = _service.Ejecutar(request);
            //Assert
            Assert.AreEqual($"Su Nuevo saldo es 1000.", response.Mensaje);
        }

        [Test]
        public void TestConsignarCorriente()
        {
            //Arrange
            ConsignarService _service = new ConsignarService(_unitOfWork, _repositoryCuentaBancaria);
            var request = new ConsignarRequest() { NumeroCuenta = "524256", Valor = 1000 };
            //Act
            ConsignarResponse response = _service.Ejecutar(request);
            //Assert
            Assert.AreEqual($"Su Nuevo saldo es 1000.", response.Mensaje);
        }

        [Test]
        public void TestTrasaldar()
        {
            //Arrange
            ConsignarService _serviceConsignar = new ConsignarService(_unitOfWork, _repositoryCuentaBancaria);
            var requestConsignar = new ConsignarRequest() { NumeroCuenta = "524255", Valor = 5000 };
            _serviceConsignar.Ejecutar(requestConsignar);

            TrasladarService _service = new TrasladarService(_unitOfWork, _repositoryCuentaBancaria);
            var request = new TrasladarRequest() { NumeroCuenta = "524255", Valor = 1000, NumeroCuentaDestino= "524256" };
            //Act
            TrasladarResponse response = _service.Ejecutar(request);
            //Assert
            Assert.AreEqual($"Su nuevo saldo es 4000.", response.Mensaje);
        }

        [Test]
        public void TestRetirarCorriente()
        {
            //Arrange
            RetirarService _service = new RetirarService(_unitOfWork, _repositoryCuentaBancaria);
            var request = new RetirarRequest() { NumeroCuenta = "524256", Valor = 500 };
            //Act
            RetirarResponse response = _service.Ejecutar(request);
            //Assert
            Assert.AreEqual($"Su nuevo saldo es -500.", response.Mensaje);
        }

    }
}
