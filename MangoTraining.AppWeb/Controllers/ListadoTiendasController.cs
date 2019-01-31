using MangoTraining.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangoTraining.AppWeb.Controllers
{
    public class ListadoTiendasController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            try
            {
                //throw new Exception("Este es mi error!!!");

                //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                //using (var clientContext = spContext.CreateUserClientContextForSPHost())
                //{
                //    if (clientContext != null)
                //    {
                //        var tiendas = new StoreCollection();
                //        tiendas.GetFromSharePoint(clientContext);
                //        ViewBag.Stores = tiendas.ToList();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}
