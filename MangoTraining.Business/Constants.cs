using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace MangoTraining.Business
{
    public class Constants
    {
        public class Columns
        {
            public const string Titulo = "Title";
            public const string Responsable = "mango_responsable";
            public const string FechaApertura = "mango_fecha";
            public const string Pais = "mango_pais";
            public const string TipoTienda = "mango_tipoTienda";
        }

        public class Lists
        {
            public const string Tiendas = "Tiendas";
        }

        public class Taxonomy
        {
            public const string Pais = "Paises";
        }

        public class AzureAD
        {
            // This is the URL the application will authenticate at.
            public const string AuthString = "https://login.windows.net/mangoform1.onmicrosoft.com";

            // These are the credentials the application will present during authentication
            // and were retrieved from the Azure Management Portal.
            // *** Don't even try to use these - they have been deleted.
            public const string ClientID = "4f32b71a-e4ec-4b87-9777-960bdebdd236";

            public const string ClientSecret = "nseu9UJA8FRCloLjTvZL3+bxR6ViX72xQhhF2KIv9wk=";

            // The Azure AD Graph API is the "resource" we're going to request access to.
            public const string ResAzureGraphAPI = "https://graph.windows.net";

            // The Azure AD Graph API for my directory is available at this URL.
            public const string ServiceRootURL = "https://graph.windows.net/mangoform1.onmicrosoft.com";

            public static ExtensionProperty extensionProperty = new ExtensionProperty()
            {
                Name = "TiendaAsociada",
                DataType = "String",
                TargetObjects = { "User" }
            };

            public static string GetFormattedExtensionPropertyName(string property)
            {
                return string.Format("extension_{0}_{1}", Constants.AzureAD.ClientID.Replace("-", ""), property);
            }
        }
    }
}