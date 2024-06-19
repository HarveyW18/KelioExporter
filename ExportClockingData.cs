using KelioDataEx.ClockingServiceClient;
using System;

namespace KelioDataEx
{
    internal class ExportClockingData
    {
        // Méthode pour se connecter au service ClockingServiceClient
        private static ClockingServiceClient.ClockingService ClockingServiceClientConnexion()
        {
            ClockingServiceClient.ClockingService clockingService = new ClockingServiceClient.ClockingService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector.ConfigureApiService(
                clockingService,
                "https://georges-helfer-sa.kelio.io/open/services/ClockingService",
                "https://georges-helfer-sa.kelio.io/open/services/ClockingServiceHttpPort"
            );
            return clockingService;
        }

        // Méthode pour obtenir les données de pointage
        public static Clocking[] GetClockingData()
        {
            ClockingServiceClient.ClockingService clockingService = ClockingServiceClientConnexion();
            DateTime startDate = new DateTime(2024, 06, 12);
            DateTime endDate = new DateTime(2024, 06, 13);

            // Créer l'objet AskedPopulationWithPeriod avec les dates
            ClockingServiceClient.AskedPopulationWithPeriod askedPopulation = new ClockingServiceClient.AskedPopulationWithPeriod
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

            ClockingServiceClient.AskedPopulationWithPeriod[] requiredList = new ClockingServiceClient.AskedPopulationWithPeriod[1];
            requiredList[0] = askedPopulation;

            return clockingService.exportCalculatedClockingsOnly(requiredList);
        }


    }
}
