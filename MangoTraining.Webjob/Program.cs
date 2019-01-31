using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangoTraining.Business;

namespace MangoTraining.Webjob
{
    class Program
    {
        public const string userName = "admin@mangoform1.onmicrosoft.com";
        public const string passWord = "P@ssw0rd!";
        public const string webURL = "https://mangoform1.sharepoint.com/sites/formacion1";

        static void Main(string[] args)
        {
            try
            {
                using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
                {
                    var tienda = new Store()
                    {
                        Description = string.Format("Prueba de creación a las {0}", DateTime.Now.ToShortTimeString()),
                        Responsable = "admin@mangoform1.onmicrosoft.com",
                        Provincia = "España",
                        OpenDate = DateTime.Now,
                        Tipo = Store.TipoTienda.Franquicias
                    };

                    tienda.SaveInSharePoint(ctx);
                    Console.WriteLine("Tienda creada con ID: {0}", tienda.key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:{0}", ex.Message);
            }
        }
    }
}
