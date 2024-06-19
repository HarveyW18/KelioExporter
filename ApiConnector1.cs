using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace KelioExporter
{
    internal class ApiConnector1
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
