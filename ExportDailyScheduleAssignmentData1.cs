using KelioExporter.DailyScheduleAssignmentServiceClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelioExporter
{
    internal class ExportDailyScheduleAssignmentData1
    {
        // Méthode pour se connecter au service EmployeeProfessionalDataServiceClient
        private static DailyScheduleAssignmentService DailyScheduleAssignmentServiceClientConnexion()
        {
            DailyScheduleAssignmentService vDailyScheduleAssignmentService = new DailyScheduleAssignmentService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector1.ConfigureApiService(
                vDailyScheduleAssignmentService,
                "https://georges-helfer-sa.kelio.io/open/services/DailyScheduleAssignmentService",
                "https://georges-helfer-sa.kelio.io/open/services/DailyScheduleAssignmentServiceHttpPort"
            );
            return vDailyScheduleAssignmentService;
        }

        // Méthode pour obtenir les données professionnelles des employés
        public static DailyScheduleAssignment[] GetDailyScheduleAssignmentData()
        {
            DailyScheduleAssignmentServiceClient1.DailyScheduleAssignmentService DailyScheduleAssignmentService = DailyScheduleAssignmentServiceClientConnexion();

            DateTime startDate = new DateTime(2024, 05, 27);
            DateTime endDate = DateTime.Now;

            DailyScheduleAssignmentServiceClient1.AskedPopulationWithPeriod askedPopulation = new DailyScheduleAssignmentServiceClient1.AskedPopulationWithPeriod
            {
                // Filtres valeurs
                populationMode = 0,
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

            DailyScheduleAssignmentServiceClient1.AskedPopulationWithPeriod[] requiredList = new DailyScheduleAssignmentServiceClient1.AskedPopulationWithPeriod[1];
            requiredList[0] = askedPopulation;

            return DailyScheduleAssignmentService.exportDailyScheduleAssignmentsList(requiredList);
        }
    }
}
