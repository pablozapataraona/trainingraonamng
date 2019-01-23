using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MangoTraining.AppWeb.Controllers
{
    public class TicketsController : ApiController
    {
        [Route("api/Stores/{storeId}/tickets")]
        // GET: api/Tickets
        public IEnumerable<string> Get(int storeId)
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/Stores/{storeId}/tickets/{id}")]
        // GET: api/Tickets/5
        public string Get(int storeId, int id)
        {
            return "value";
        }

        // POST: api/Tickets
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tickets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tickets/5
        public void Delete(int id)
        {
        }
    }
}
