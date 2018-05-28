using Application.Implements;
using Domain.Entities;
using Infraestructura.Data.Base;
using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using SirccELC.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            BancoContext context = new BancoContext();
            #region  Crear Country
            //CountryService service = new CountryService(new UnitOfWork(context), new CountryRepository(context));

            //Country country = new Country() { Name = "Colombia" };

            //service.Create(country);

            //List<Country> countries=service.GetAll().ToList();

            //foreach (var item in countries)
            //{
            //    System.Console.WriteLine(item.Name);
            //}
            #endregion

            CrearCuentaBancaria(context);
            ConsignarCuentaBancaria(context);
        }

        private static void ConsignarCuentaBancaria(BancoContext context)
        {
            #region  Consignar

            ConsignarService _service = new ConsignarService(new UnitOfWork(context), new CuentaBancariaRepository(context));
            var request = new ConsignarRequest() { NumeroCuenta = "524255", Valor = 1000 };

            ConsignarResponse response = _service.Ejecutar(request);

            System.Console.WriteLine(response.Mensaje);
            #endregion
            System.Console.ReadKey();
        }

        private static void CrearCuentaBancaria(BancoContext context)
        {
            #region  Crear

            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(context), new CuentaBancariaRepository(context));
            var requestCrer = new CrearCuentaBancariaRequest() { Numero = "524255", Nombre = "Boris Arturo" };

            CrearCuentaBancariaResponse responseCrear = _service.Ejecutar(requestCrer);

            System.Console.WriteLine(responseCrear.Mensaje);
            #endregion
        }
    }
}
