using KelioDataEx.EmployeeProfessionalDataServiceClient;

namespace KelioDataEx
{
    internal class ExportProfessionalData
    {
        // Méthode pour se connecter au service EmployeeProfessionalDataServiceClient
        private static EmployeeProfessionalDataService EmployeeProfessionalDataServiceClientConnexion()
        {
            EmployeeProfessionalDataService vEmployeeProfessionalDataService = new EmployeeProfessionalDataService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector.ConfigureApiService(
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

            EmployeeProfessionalDataServiceClient.AskedPopulation AskedPopulation = new EmployeeProfessionalDataServiceClient.AskedPopulation
            {
                populationMode = 0,
                populationFilter = null,
                groupFilter = null,
                populationModeSpecified = true
            };

            EmployeeProfessionalDataServiceClient.AskedPopulation[] RequiredList = new EmployeeProfessionalDataServiceClient.AskedPopulation[1];
            RequiredList[0] = AskedPopulation;

            return EmployeeProfessionalDataService.exportEmployeeProfessionalDataList(RequiredList);
        }
    }
}
