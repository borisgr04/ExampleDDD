using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test
{
    [TestFixture]
    public class TestConsignar
    {
        [Test]
        public void TestCorrecta()
        {
            CuentaAhorro cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Consignar(100000);
            // TODO: Add your test code here
            Assert.AreEqual(100000,cuentaAhorro.Saldo);
        }
    }
}
