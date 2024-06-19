using KelioExporter.ClockingServiceClient1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelioExporter
{
    internal class ExportClockingData1
    {
        // Méthode pour se connecter au service ClockingServiceClient
        private static ClockingServiceClient1.ClockingService ClockingServiceClientConnexion()
        {
            ClockingServiceClient1.ClockingService clockingService = new ClockingServiceClient1.ClockingService();
            // Utiliser la classe Connection pour configurer le service
            ApiConnector1.ConfigureApiService(
                clockingService,
                "https://georges-helfer-sa.kelio.io/open/services/ClockingService",
                "https://georges-helfer-sa.kelio.io/open/services/ClockingServiceHttpPort"
            );
            return clockingService;
        }

        // Méthode pour obtenir les données de pointage
        public static Clocking[] GetClockingData()
        {
            ClockingServiceClient1.ClockingService clockingService = ClockingServiceClientConnexion();

            DateTime startDate = new DateTime(2024, 05, 27);
            DateTime endDate = DateTime.Now;


            // Créer l'objet AskedPopulationWithPeriod avec les dates
            ClockingServiceClient1.AskedPopulationWithPeriod askedPopulation = new ClockingServiceClient1.AskedPopulationWithPeriod
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

            ClockingServiceClient1.AskedPopulationWithPeriod[] requiredList = new ClockingServiceClient1.AskedPopulationWithPeriod[1];
            requiredList[0] = askedPopulation;

            return clockingService.exportClockings(requiredList);
        }

    }
}
