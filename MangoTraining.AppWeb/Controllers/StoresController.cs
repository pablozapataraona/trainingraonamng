using MangoTraining.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MangoTraining.AppWeb.Controllers
{
    public class StoresController : ApiController
    {
        public const string userName = "admin@mangoform1.onmicrosoft.com";
        public const string passWord = "P@ssw0rd!";
        public const string webURL = "https://mangoform1.sharepoint.com/sites/formacion1";

        // GET: api/Stores
        public HttpResponseMessage Get()
        {
            try
            {
                var listadoTiendas = new StoreCollection();
                using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
                {
                    listadoTiendas.GetFromSharePoint(ctx);
                }

                return Request.CreateResponse(HttpStatusCode.OK, listadoTiendas);
            }
            catch(Exception ex)
            {
                var msg = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };

                return msg;
            }

        }

        // GET: api/Stores/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stores
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Stores/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stores/5
        public void Delete(int id)
        {
        }

        //[Route("api/Stores/{id}/tickets")]
        //public HttpResponseMessage GetTickets(int id)
        //{
        //    try
        //    {
        //        var tickets = new string[] { "value1", "value2" };
        //        return Request.CreateResponse(HttpStatusCode.OK, tickets);
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.InternalServerError,
        //            Content = new StringContent(ex.Message)
        //        };

        //        return msg;
        //    }

        //}
    }
}
