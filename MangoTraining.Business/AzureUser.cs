using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Data.OData;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MangoTraining.Business
{
    public class AzureUser
    {
        public string AccountUPN { get; set; }
        public string TiendaAsociada { get; set; }

        public void SetUserCustomPropertiesInAD()
        {
            if (string.IsNullOrEmpty(AccountUPN)) throw new Exception("User account cannot be empty!");

            // Set the extension property value for a user
            var upn = this.AccountUPN;

            User user = (User)AzureUserCollection.ADClient.Users.Where(u => u.UserPrincipalName.Equals(
                upn, StringComparison.CurrentCultureIgnoreCase)).ExecuteSingleAsync().Result;

            if (user != null)
            {
                var propertyName = Constants.AzureAD.GetFormattedExtensionPropertyName(Constants.AzureAD.extensionProperty.Name);
                user.SetExtendedProperty(propertyName, this.TiendaAsociada);
                user.UpdateAsync();
                Task.WaitAll();

                // Save the extended property value to Azure AD.
                user.GetContext().SaveChanges();
            }
        }

        public void GetUserPropertiesFromAD()
        {
            var upn = this.AccountUPN;

            User user = (User)AzureUserCollection.ADClient.Users.Where(u => u.UserPrincipalName.Equals(
                upn, StringComparison.CurrentCultureIgnoreCase)).ExecuteSingleAsync().Result;

            var propertyName = Constants.AzureAD.GetFormattedExtensionPropertyName(Constants.AzureAD.extensionProperty.Name);
            var properties = user.GetExtendedProperties();

            var tienda = properties.FirstOrDefault(x => x.Key == propertyName);

            if (tienda.Value != null)
            {
                this.TiendaAsociada = tienda.Value.ToString();
            }
        }
    }

    public class AzureUserCollection : List<AzureUser>
    {
        private static Microsoft.Azure.ActiveDirectory.GraphClient.Application _AzureApp;

        public static Microsoft.Azure.ActiveDirectory.GraphClient.Application AzureApp
        {
            get
            {
                Microsoft.Azure.ActiveDirectory.GraphClient.Application app =
                    (Microsoft.Azure.ActiveDirectory.GraphClient.Application)ADClient.Applications.Where(a => a.AppId == Constants.AzureAD.ClientID).ExecuteSingleAsync().Result;

                if (app == null)
                {
                    throw new ApplicationException("Unable to get a reference to application in Azure AD.");
                }

                _AzureApp = app;
                return _AzureApp;
            }
            set => _AzureApp = value;
        }

        private static ActiveDirectoryClient _ADClient;

        public static ActiveDirectoryClient ADClient
        {
            get
            {
                if (_ADClient == null)
                {
                    Uri serviceRoot = new Uri(Constants.AzureAD.ServiceRootURL);
                    ActiveDirectoryClient adClient = new ActiveDirectoryClient(
                        serviceRoot,
                        async () => await GetAppTokenAsync());
                    adClient.Context
                              .Configurations.RequestPipeline
                              .OnMessageWriterSettingsCreated(UndeclaredPropertyHandler);
                    _ADClient = adClient;
                }

                return _ADClient;
            }
        }

        /// <summary>
        /// Esto viene de este bug: https://github.com/Azure-Samples/active-directory-dotnet-graphapi-console/issues/28
        /// </summary>
        /// <param name="args"></param>
        private static void UndeclaredPropertyHandler(MessageWriterSettingsArgs args)
        {
            var field = args.Settings.GetType().GetField("settings",
              BindingFlags.NonPublic | BindingFlags.Instance);
            var settingsObject = field?.GetValue(args.Settings);
            var settings = settingsObject as ODataMessageWriterSettings;
            if (settings != null)
            {
                settings.UndeclaredPropertyBehaviorKinds =
                   ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty;
            }
        }

        public static void ExtendAzureADProperties()
        {
            AzureApp.ExtensionProperties.Add(Constants.AzureAD.extensionProperty);
            AzureApp.UpdateAsync();
            Task.WaitAll();

            // Apply the change to Azure AD
            AzureApp.GetContext().SaveChanges();
        }

        public static List<string> GetExtendedPropertiesFromAD()
        {
            
            IEnumerable<IExtensionProperty> allExtProperties = ADClient.GetAvailableExtensionPropertiesAsync(false).Result;
            var props = allExtProperties.ToList();
            var retProperties = new List<string>();

            foreach (var prop in props)
            {
                retProperties.Add(prop.Name);
            }

            return retProperties;
        }

        private static async Task<string> GetAppTokenAsync()
        {
            // Instantiate an AuthenticationContext for my directory (see authString above).
            AuthenticationContext authenticationContext = new AuthenticationContext(Constants.AzureAD.AuthString, false);

            // Create a ClientCredential that will be used for authentication.
            // This is where the Client ID and Key/Secret from the Azure Management Portal is used.
            ClientCredential clientCred = new ClientCredential(Constants.AzureAD.ClientID, Constants.AzureAD.ClientSecret);

            // Acquire an access token from Azure AD to access the Azure AD Graph (the resource)
            // using the Client ID and Key/Secret as credentials.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(Constants.AzureAD.ResAzureGraphAPI, clientCred);

            // Return the access token.
            return authenticationResult.AccessToken;
        }
    }
}