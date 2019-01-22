using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangoTraining.Business
{
    public class SharePoint
    {
        public static ClientContext GetContext(string userName, string passWord, string webURL)
        {
            System.Security.SecureString pass = new System.Security.SecureString();
            foreach (char c in passWord.ToCharArray()) pass.AppendChar(c);
            ClientContext clientcontext = new ClientContext(webURL);
            clientcontext.Credentials = new SharePointOnlineCredentials(userName, pass);
            clientcontext.ExecuteQuery();
            return clientcontext;
        }

        public static string GetLabelFromTaxonomyObject(object taxonomy)
        {
            if (taxonomy != null)
            {
                if (taxonomy.GetType() == typeof(TaxonomyFieldValue))
                {
                    return ((TaxonomyFieldValue)taxonomy).Label;
                }
                else
                {
                    return ((System.Collections.Generic.Dictionary<string, object>)taxonomy)["Label"].ToString();
                }
            }

            throw new Exception("Taxonomy item is null");
        }

    }

    public static class ClientContextExtension
    {
        // This is the extension method. 
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined. 
        /// <summary>
        /// Extension method to invoke execute query with retry and incremental back off.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="retryCount">Maximum amount of retries before giving up.</param>
        /// <param name="delay">Initial delay in milliseconds.</param>
        public static void ExecuteQueryWithIncrementalRetry(this ClientContext context)
        {
            int retryCount = 5;
            int delay = 10000;
            int retryAttempts = 0;
            int backoffInterval = delay;
            context.RequestTimeout = System.Threading.Timeout.Infinite;
            //if (retryCount <= 0)
            //    throw new ArgumentException("Provide a retry count greater than zero.");

            //if (delay <= 0)
            //    throw new ArgumentException("Provide a delay greater than zero.");

            // Do while retry attempt is less than retry count
            while (retryAttempts < retryCount)
            {
                try
                {
                    context.ExecuteQuery();
                    return;
                }
                catch (WebException wex)
                {
                    var response = wex.Response as HttpWebResponse;
                    // Check if request was throttled - http status code 429
                    // Check is request failed due to server unavailable - http status code 503
                    if (response != null && (response.StatusCode == (HttpStatusCode)429 || response.StatusCode == (HttpStatusCode)503))
                    {
                        // Output status to console. Should be changed as Debug.WriteLine for production usage.
                        System.Diagnostics.Trace.TraceWarning(string.Format("CSOM request frequency exceeded usage limits. Sleeping for {0} seconds before retrying.", backoffInterval));

                        //Add delay for retry
                        System.Threading.Thread.Sleep(backoffInterval);

                        //Add to retry count and increase delay.
                        retryAttempts++;
                        backoffInterval = backoffInterval * 2;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            throw new MaximumRetryAttemptedException(string.Format("Maximum retry attempts {0}, has be attempted.", retryCount));
        }

        public class MaximumRetryAttemptedException : Exception
        {
            public MaximumRetryAttemptedException(string message)
                : base(message)
            {

            }
        }

    }
}
