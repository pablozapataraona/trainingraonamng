using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangoTraining.Business;

namespace MangoTraining.UpdateUserProfileProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Extendemos la propiedad en Azure para que esté disponible 
                //AzureUserCollection.ExtendAzureADProperties();

                //Listamos todas las propiedades extendidas del AD
                var allProperties = AzureUserCollection.GetExtendedPropertiesFromAD();
                foreach (var property in allProperties)
                {
                    Console.WriteLine(property);
                }

                //Asignamos la propiedad a un usuario
                var userTest = new AzureUser()
                {
                    AccountUPN = "j.doe@mangoform1.onmicrosoft.com",
                    TiendaAsociada = "Barcelona Paseo de gracia"
                };
                userTest.SetUserCustomPropertiesInAD();

                //Obtenemos y pintamos las propiedades del usuario
                userTest.GetUserPropertiesFromAD();
                Console.WriteLine(userTest.TiendaAsociada);


                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error has occurred");
                Console.WriteLine(ex.Message);
                while(ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
