using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CountryController : ApiController
    {
        // GET: api/Country
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Country/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Country
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Country/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Country/5
        public void Delete(int id)
        {
        }
    }
}
