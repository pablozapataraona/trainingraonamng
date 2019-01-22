using Microsoft.SharePoint.Client;
using System;

namespace HelloWorld
{
    internal class Program
    {
        public const string userName = "admin@mangoform1.onmicrosoft.com";
        public const string passWord = "P@ssw0rd!";
        public const string webURL = "https://mangoform1.sharepoint.com/sites/formacion1";

        private static void Main(string[] args)
        {
            try
            {
                var exit = false;

                while (exit == false)
                {
                    var keyPressed = PrintMenu();

                    switch (keyPressed.Key)
                    {
                        case (ConsoleKey.D0):
                            Console.WriteLine("Hello world!!!");
                            break;

                        case (ConsoleKey.D1):
                            PrintUserArgs(args);
                            break;

                        case (ConsoleKey.D2):
                            throw new Exception("Esta es una excepción de error!!!!");
                        case (ConsoleKey.D3):
                            PrintStores();
                            break;

                        case (ConsoleKey.D4):
                            PrintEmployeesFromCollection();
                            break;

                        case (ConsoleKey.D5):
                            CheckNIF();
                            break;

                        case (ConsoleKey.D6):
                            GetSharePointContext();
                            break;

                        case (ConsoleKey.D7):
                            GetListadoTiendas();
                            break;

                        case (ConsoleKey.D8):
                            SaveTienda();
                            break;

                        case (ConsoleKey.D9):
                            GetTienda();
                            break;

                        case (ConsoleKey.U):
                            UpdateTienda();
                            break;
                        case (ConsoleKey.D):
                            DeleteTienda();
                            break;
                        case (ConsoleKey.X):
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("No valid option selected, :'( ");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //Concatenamos strings, esta es la forma más óptima
                Console.ForegroundColor = ConsoleColor.Red;
                var error = string.Format("An error has occurred: {0}", ex.Message);

                //Concatenamos strings, pero aquí es mucho menos óptimo aunque el resultado es el mismo
                var error2 = "An error has occurred " + ex.Message;
                Console.WriteLine(error);

                Console.ForegroundColor = ConsoleColor.Gray;
            }
            finally
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ha finalizado la ejecución, pulse una tecla para continuar...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();
            }
        }

        private static void PrintUserArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args)
                {
                    Helper.PrintInConsole(arg);
                }
            }
            else
            {
                Helper.PrintInConsole("No args", ConsoleColor.Red);
            }
        }

        private static ConsoleKeyInfo PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**********   Mango Improved Hello World v.1.0 ********************");
            Console.WriteLine("0) Imprimir Hello world!");
            Console.WriteLine("1) Imprimir argumentos del usuario");
            Console.WriteLine("2) Lanzar error de ejemplo");
            Console.WriteLine("3) Imprimir listado tiendas");
            Console.WriteLine("4) Imprimir listado empleados colección");
            Console.WriteLine("5) Probar si el NIF introducido es válido");
            Console.WriteLine("6) Coger contexto SharePoint");
            Console.WriteLine("7) Mostrar listado tiendas");
            Console.WriteLine("8) Crear nueva tienda");
            Console.WriteLine("9) Obtener tienda por ID");
            Console.WriteLine("U) Actualizar tienda");
            Console.WriteLine("D) Eliminar elemento");
            Console.WriteLine("X) Salir");
            Console.WriteLine("*******************************************************************");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Por favor, escoja una opción...");
            Console.ForegroundColor = ConsoleColor.Gray;

            var keyPressed = Console.ReadKey(true);
            return keyPressed;
        }

        private static void PrintStores()
        {
            //Definimos las propiedades una a una
            var tienda1 = new Store(1);
            tienda1.OpenDate = DateTime.Today;
            tienda1.Description = "Esta es mi descripción";
            tienda1.SetStoreID(11);
            tienda1.Tipo = Store.TipoTienda.Deposito;

            //Definimos las propiedades inline
            var tienda2 = new Store(2)
            {
                OpenDate = DateTime.Today,
                Description = "Esta es la descripción de la tienda 2",
                Tipo = Store.TipoTienda.Franquicias
            };

            //Creación de objeto con inicialización en constructor
            var tienda4 = new Store(4)
            {
                OpenDate = DateTime.Today,
                Description = "Esta tienda la he inicializado con el 4",
                Tipo = Store.TipoTienda.Propias
            };

            //inicializo la colección sin elementos
            var tiendaCol1 = new StoreCollection();
            tiendaCol1.Add(tienda1);
            tiendaCol1.Add(tienda2);
            tiendaCol1.Add(new Store(1) { OpenDate = new DateTime(2019, 1, 15), Description = "Esta es mi descripción 3" });
            tiendaCol1.Add(tienda4);

            //Inicializo la colección con los elementos de forma explícita
            var tiendaCol2 = new StoreCollection()
            {
                tienda1,
                tienda2,
                new Store(3) { OpenDate = DateTime.Today, Description= "Esta es mi descripción 3"}
            };

            //foreach(var store in tiendaCol1)
            //{
            //    Console.WriteLine("ID: {0}, Fecha: {1}, Descripción: {2}", store.ID, store.OpenDate.ToShortDateString(), store.Description);
            //}

            foreach (var store in tiendaCol1.GetByDate(DateTime.Today))
            {
                Console.WriteLine("ID: {0}, Fecha: {1}, Descripción: {2}", store.ID, store.OpenDate.ToShortDateString(), store.Description);
            }
        }

        public static void PrintEmployeesFromCollection()
        {
            var employeeCollection = new EmployeeCollection()
            {
                new Employee() { NIF = "B213123123", TiendaAsociada = new Store(1) },
                new Employee() { NIF = "B21348733", TiendaAsociada = new Store(1) },
                new Employee() { NIF = "B213425363", TiendaAsociada = new Store(2) }
            };

            //Llamada no estática
            employeeCollection.GetByStoreId(1);

            //llamada estática
            var employeeCollectionStatic = EmployeeCollection.GetByStoreIdStatic(1);
        }

        public static void AreNIFOk()
        {
            var employeeCollection = new EmployeeCollection()
            {
                new Employee() { NIF = "B213123123", TiendaAsociada = new Store(1) },
                new Employee() { NIF = "B21348733", TiendaAsociada = new Store(1) },
            };

            ////Resultado no estático
            //foreach(var emp in employeeCollection)
            //{
            //    var res = emp.IsNIFOk();
            //}

            //foreach (var emp in employeeCollection)
            //{
            //    var res = Employee.IsNIFOkStatic(emp.NIF);
            //}
        }

        public static void CheckNIF()
        {
            var isValid = false;

            while (isValid == false)
            {
                try
                {
                    Console.WriteLine("Introduzca el NIF a continuación");
                    var nif = Console.ReadLine();

                    /*
                    var employee = new Employee();
                    employee.NIF = nif;
                    */
                    isValid = Employee.IsNIFValid(nif);

                    if (isValid)
                    {
                        Console.WriteLine("Felicidades, nif válido!!!");
                    }
                    else
                    {
                        Console.WriteLine("No es válido");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void GetSharePointContext()
        {
            using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
            {
                var sitioRaiz = ctx.Web;
                //ctx.Load(ctx.Web);

                ctx.Load(ctx.Web, web => web.Title, web => web.Url);
                ctx.Load(ctx.Web, web => web.Lists.Include(listahija => listahija.Title));
                ctx.ExecuteQuery();

                var title = ctx.Web.Title;
                Console.WriteLine("El título del sitio es: {0}", title);

                //Obtener un listado ÓPTIMO
                var listadoTiendas = sitioRaiz.Lists.GetByTitle("Tiendas");
                ctx.Load(listadoTiendas, x => x.Title);
                ctx.ExecuteQuery();

                //Obtener un listado NO ÓPTIMO
                List listadoNoOptimo = null;
                foreach (var list in sitioRaiz.Lists)
                {
                    if (list.Title == "Tiendas")
                    {
                        listadoNoOptimo = list;
                        break;
                    }
                }

                //Obtener todos los listados del sitio

                var todosLosListados = sitioRaiz.Lists;

                foreach (var list in todosLosListados)
                {
                    Console.WriteLine("Título del listado {0}", list.Title);
                }
            }
        }

        public static void GetListadoTiendas()
        {
            using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
            {
                var listadoTiendas = new StoreCollection();
                listadoTiendas.GetFromSharePoint(ctx);

                foreach (var tienda in listadoTiendas)
                {
                    PrintTienda(tienda);
                }
            }
        }

        private static void PrintTienda(Store tienda)
        {
            Console.WriteLine("Tienda -> ID:{0}, descripción:{1}, fecha:{2}, responsable:{3}, provincia:{4}, tipo:{5}, contenido:{6}",
                tienda.ID, tienda.Description, tienda.OpenDate.ToShortDateString(), tienda.Responsable, tienda.Provincia, tienda.Tipo, tienda.TipoDeContenido);
        }

        private static void SaveTienda()
        {
            using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
            {
                var tienda = new Store()
                {
                    Description = "Esta es mi nueva store!",
                    Responsable = "admin@mangoform1.onmicrosoft.com",
                    Provincia = "España",
                    OpenDate = DateTime.Now,
                    Tipo = Store.TipoTienda.Franquicias
                };

                tienda.SaveInSharePoint(ctx);
                Console.WriteLine("Tienda creada con ID: {0}", tienda.ID);
            }
        }

        private static void GetTienda()
        {
            Console.WriteLine("Introduzca el ID de tienda:");
            var keyPressed = Console.ReadLine();

            if (int.TryParse(keyPressed, out int id) == true)
            {
                using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
                {
                    var store = new Store();
                    store.GetById(id, ctx);
                    PrintTienda(store);
                }
            }
            else
            {
                throw new Exception("Por favor, incluya un entero.");
            }
        }

        private static void UpdateTienda()
        {
            Console.WriteLine("Introduzca el ID de tienda:");
            var keyPressed = Console.ReadLine();

            if (int.TryParse(keyPressed, out int id) == true)
            {
                using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
                {
                    var store = new Store();
                    store.GetById(id, ctx);
                    store.Description = string.Format("Descripción actualizada {0}", DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy"));
                    store.OpenDate = DateTime.Now;
                    store.Update(ctx);

                    PrintTienda(store);
                }
            }
            else
            {
                throw new Exception("Por favor, incluya un entero.");
            }
        }

        private static void DeleteTienda()
        {
            Console.WriteLine("Introduzca el ID de tienda:");
            var keyPressed = Console.ReadLine();

            if (int.TryParse(keyPressed, out int id) == true)
            {
                using (var ctx = SharePoint.GetContext(userName, passWord, webURL))
                {
                    var store = new Store();
                    store.GetById(id, ctx);
                    store.DeleteFromSharePoint();

                    PrintTienda(store);
                }
            }
            else
            {
                throw new Exception("Por favor, incluya un entero.");
            }
        }
    }
}