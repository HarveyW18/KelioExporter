using KelioDataEx.DailyScheduleAssignmentServiceClient;
using System;

namespace KelioDataEx
{
    internal class ExportDailyScheduleAssignmentData
    {
        // Méthode pour se connecter au service EmployeeProfessionalDataServiceClient
        private static DailyScheduleAssignmentService DailyScheduleAssignmentServiceClientConnexion()
        {
            DailyScheduleAssignmentService vDailyScheduleAssignmentService = new DailyScheduleAssignmentService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector.ConfigureApiService(
                vDailyScheduleAssignmentService,
                "https://georges-helfer-sa.kelio.io/open/services/DailyScheduleAssignmentService",
                "https://georges-helfer-sa.kelio.io/open/services/DailyScheduleAssignmentServiceHttpPort"
            );
            return vDailyScheduleAssignmentService;
        }

        // Méthode pour obtenir les données professionnelles des employés
        public static DailyScheduleAssignment[] GetDailyScheduleAssignmentData()
        {
            DailyScheduleAssignmentServiceClient.DailyScheduleAssignmentService DailyScheduleAssignmentService = DailyScheduleAssignmentServiceClientConnexion();

            DateTime startDate = new DateTime(2024, 05, 27);
            DateTime endDate = new DateTime(2024, 06, 02);

            DailyScheduleAssignmentServiceClient.AskedPopulationWithPeriod askedPopulation = new DailyScheduleAssignmentServiceClient.AskedPopulationWithPeriod
            {
                // Filtres valeurs
                populationMode = 0,
                populationFilter = null,
                groupFilter = null,
                startDate = startDate,
                endDate = endDate,
                dateMode = 0,

                // Filtres booléens
                populationModeSpecified = true,
                startDateSpecified = true,
                endDateSpecified = true,
                dateModeSpecified = true
            };

            DailyScheduleAssignmentServiceClient.AskedPopulationWithPeriod[] requiredList = new DailyScheduleAssignmentServiceClient.AskedPopulationWithPeriod[1];
            requiredList[0] = askedPopulation;

            return DailyScheduleAssignmentService.exportDailyScheduleAssignmentsList(requiredList);
        }
    }
}
