using Application.Base;
using Application.Implements;
using Application.Test.Base;
using Domain.Abstracts;
using Domain.Entities;
using Infraestructura.Data.Base;
using Infraestructure.Data;
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
    public class OperacionesFinancierasServiceDbTest
    {
        List<CuentaBancaria> _listCuentaBancaria;
        IUnitOfWork _unitOfWork;
        IDbContext _db;
        ICuentaBancariaRepository _repositoryCuentaBancaria;

        [SetUp]
        public void Initializar()
        {
            _db = new BancoContext();
            _listCuentaBancaria = new List<CuentaBancaria>() {
               new CuentaAhorro() { Id = 1, Numero="524255",Nombre="Cuenta 1"},
               new CuentaCorriente() { Id = 2, Numero="524256",Nombre="Cuenta 2"},
            };
            _db.Set<CuentaBancaria>().AddRange(_listCuentaBancaria);
            _db.SaveChanges();
            _repositoryCuentaBancaria = new CuentaBancariaRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }

        [TearDown]
        public void Finalizar()
        {
            _repositoryCuentaBancaria.DeleteRange(_repositoryCuentaBancaria.GetAll().ToList());
            _unitOfWork.Commit();
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
