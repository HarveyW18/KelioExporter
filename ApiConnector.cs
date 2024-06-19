using System;
using System.Net;
using System.Web.Services.Protocols;

namespace KelioDataEx
{
    internal class ApiConnector
    {
        private static readonly string Username = "service";
        private static readonly string Password = "@Helfer2023..";

        public static void ConfigureApiService(SoapHttpClientProtocol service, string serviceUrl, string servicePort)
        {
            service.Url = serviceUrl;
            CredentialCache credential = new CredentialCache();
            NetworkCredential networkCredential = new NetworkCredential(Username, Password);
            credential.Add(new Uri(servicePort), "Basic", networkCredential);
            service.Credentials = credential;
            ServicePointManager.Expect100Continue = false;
        }

        public static void DatabaseConnection()
        {




        }
    }
}
