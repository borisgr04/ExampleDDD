using Application.Contracts;
using Application.Implements;
using Domain.Abstracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Net;
using System.Threading;

namespace WebApiNUnit.Tests
{
    [TestFixture]
    public class TestCountryController
    {
        private Mock<ICountryRepository> _mockRepository;
        private ICountryService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        List<Country> listCountry;
        CountryController controller;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<ICountryRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new CountryService(_mockUnitWork.Object, _mockRepository.Object);
            listCountry = new List<Country>() {
               new Country() { Id = 1, Name = "US" },
               new Country() { Id = 2, Name = "India" },
               new Country() { Id = 3, Name = "Russia" }
            };
            var ServiceBaseURL = "http://localhost:65413/";
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(listCountry);

            controller = new CountryController(_service)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ServiceBaseURL + "api/Country")
                }
            };

            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }


        [Test]
        public void Test_CountryController_Get()
        {
            // Act
            var response = controller.Get().ExecuteAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsCompleted);
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

            // Assertions on returned data
            List<Country> models;
            Assert.IsTrue(response.Result.TryGetContentValue<List<Country>>(out models));
            Assert.AreEqual(3, models.Count);

            Assert.AreEqual(1, models.First().Id);
        }
    }
    }
