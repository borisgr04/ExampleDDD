using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            IEnumerable<string> result = controller.Get();

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            string result = controller.Get(5);

            // Declarar
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Post("value");

            // Declarar
        }

        [TestMethod]
        public void Put()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Put(5, "value");

            // Declarar
        }

        [TestMethod]
        public void Delete()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Delete(5);

            // Declarar
        }
    }
}
