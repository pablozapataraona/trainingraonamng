using System;

namespace MangoTraining.Business
{
    public class Helper
    {
        /// <summary>
        /// Imprime el texto informado en pantalla
        /// </summary>
        /// <param name="textToPrint">Texto a imprimir</param>
        /// <param name="waitForKey">Esperar a pulsación del usuario</param>
        public static void PrintInConsole(string textToPrint, bool waitForKey = false)
        {
            Console.WriteLine(textToPrint);
            if (waitForKey)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Imprime el texto informado en pantalla con color
        /// </summary>
        /// <param name="textToPrint">Texto a imprimir</param>
        /// <param name="color">Color del texto a imprimir</param>
        public static void PrintInConsole(string textToPrint, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            PrintInConsole(textToPrint, true);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Permite parsear el string del valor del elemento contra el enumerable. En caso de no existir devuelve el valor del enumerable
        /// indefinido
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Store.TipoTienda GetStoreEnumFromString(string value)
        {
            var tipo = Store.TipoTienda.Indefinido;

            if (Enum.TryParse<Store.TipoTienda>(value, out tipo) == true)
            {
                return tipo;
            }
            else
            {
                return Store.TipoTienda.Indefinido;
            }
        }
    }
}