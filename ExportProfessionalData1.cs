using KelioExporter.EmployeeProfessionalDataServiceClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelioExporter
{
    internal class ExportProfessionalData1
    {
        // Méthode pour se connecter au service EmployeeProfessionalDataServiceClient
        private static EmployeeProfessionalDataService EmployeeProfessionalDataServiceClientConnexion()
        {
            EmployeeProfessionalDataService vEmployeeProfessionalDataService = new EmployeeProfessionalDataService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector1.ConfigureApiService(
                vEmployeeProfessionalDataService,
                "https://georges-helfer-sa.kelio.io/open/services/EmployeeProfessionalDataService",
                "https://georges-helfer-sa.kelio.io/open/services/EmployeeProfessionalDataServiceHttpPort"
            );
            return vEmployeeProfessionalDataService;
        }

        // Méthode pour obtenir les données professionnelles des employés
        public static EmployeeProfessionalData[] GetEmployeeProfessionalData()
        {
            EmployeeProfessionalDataService EmployeeProfessionalDataService = EmployeeProfessionalDataServiceClientConnexion();

            EmployeeProfessionalDataServiceClient1.AskedPopulation AskedPopulation = new EmployeeProfessionalDataServiceClient1.AskedPopulation
            {
                populationMode = 0,
                populationFilter = null,
                groupFilter = null,
                populationModeSpecified = true
            };

            EmployeeProfessionalDataServiceClient1.AskedPopulation[] RequiredList = new EmployeeProfessionalDataServiceClient1.AskedPopulation[1];
            RequiredList[0] = AskedPopulation;

            return EmployeeProfessionalDataService.exportEmployeeProfessionalDataList(RequiredList);
        }
    }
}
