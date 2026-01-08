using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Configuration;

namespace DataverseCRUDOperationSamples
{
    public class DataverseService
    {
        public static IOrganizationService GetService()
        {
            string envUrl = ConfigurationManager.AppSettings["DataverseUrl"];
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

            var serviceClient = new ServiceClient(new Uri(envUrl), clientId, clientSecret, true);


            if (!serviceClient.IsReady)
            {
                throw new Exception(serviceClient.LastError);
            }

            return serviceClient;
        }
    }
}
