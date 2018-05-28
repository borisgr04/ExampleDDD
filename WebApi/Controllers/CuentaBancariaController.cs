using Application.Implements;
using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using SirccELC.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    [RoutePrefix("api/CuentaBancaria")]
    public class CuentaBancariaController : ApiController
    {
        private readonly BancoContext _db;
        public CuentaBancariaController()
        {
            _db = new BancoContext();
            
        }

        public CuentaBancariaController(BancoContext db)
        {
            this._db = db;
        }


        // POST: api/Country
        [ResponseType(typeof(ConsignarResponse))]
        [Route("Consignar")]
        public IHttpActionResult Post(ConsignarRequest request)
        {
            ConsignarService _service = new ConsignarService(new UnitOfWork(_db), new CuentaBancariaRepository(_db));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _service.Ejecutar(request);

            return Ok(response);

        }
        // POST: api/Country
        [ResponseType(typeof(CrearCuentaBancariaResponse))]
        [Route("Crear")]
        public IHttpActionResult PostCrear(CrearCuentaBancariaRequest request)
        {
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_db), new CuentaBancariaRepository(_db));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _service.Ejecutar(request);

            return Ok(response);

        }

    }
}
