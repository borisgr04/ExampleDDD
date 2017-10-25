using Domain.Entities;
using Infraestructure.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Test
{
    [TestFixture]
    public class CountryRepositoryTestWithDB
    {
        SampleArchContextTest databaseContext;
        CountryRepository objRepo;

        [SetUp]
        public void Initialize()
        {
            Console.WriteLine("Inicalizando");
            databaseContext = new SampleArchContextTest();
            objRepo = new CountryRepository(databaseContext);
        }

        [Test]
        public void Country_Repository_Get_ALL()
        {
            Console.WriteLine("Ejecutando ALL");
            
            //Act
            var result = objRepo.GetAll().ToList();

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("US", result[0].Name);
            Assert.AreEqual("India", result[1].Name);
            Assert.AreEqual("Russia", result[2].Name);
        }

        [Test]
        public void Country_Repository_Create()
        {
            Console.WriteLine("Ejecutando NEW");
            //Arrange
            Country c = new Country() { Name = "UK" };

            //Act
            objRepo.Add(c);
            databaseContext.SaveChanges();

            var lst = objRepo.GetAll().ToList();

            //Assert

            Assert.AreEqual(4, lst.Count);
            Assert.AreEqual("UK", lst.Last().Name);

            objRepo.Delete(c);
            databaseContext.SaveChanges();

        }
    }
}

