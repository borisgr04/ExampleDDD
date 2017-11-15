using Application.Contracts;
using Application.Implements;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using SirccELC.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApi.Controllers
{
    public class CountryController : ApiController
    {
        readonly ICountryService _service;

        public CountryController() {
            BancoContext _db = new BancoContext();
            _service = new CountryService(new UnitOfWork(_db), new CountryRepository(_db));
        }

        public CountryController(ICountryService service)
        {
            this._service = service;
        }

        // GET: api/Country
        [ResponseType(typeof(Country))]
        public IHttpActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/Country/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult Get(int id)
        {
            Country country = _service.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // POST: api/Country
        [ResponseType(typeof(Country))]
        public IHttpActionResult Post(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(country);

            return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
            
        }

        // PUT: api/Country/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody]Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }
            try
            {
                _service.Update(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Country/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult Delete(int id)
        {
            Country country = _service.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            _service.Delete(country);

            return Ok(country);
        }

        private bool CountryExists(int id)
        {
            return _service.Find(id) !=null;
        }
        
    }

}
