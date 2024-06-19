using System;
using System.Collections.Generic;
using System.Linq;
using KelioExporter.ClockingServiceClient1;
using KelioExporter.DailyScheduleAssignmentServiceClient1;
using KelioExporter.EmployeeProfessionalDataServiceClient1;

namespace KelioExporter
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                // Appel des méthodes pour obtenir les données de pointage et les données professionnelles
                Clocking[] vClockingList = ExportClockingData1.GetClockingData();
                EmployeeProfessionalData[] vEmployeeProfessionalDatas = ExportProfessionalData1.GetEmployeeProfessionalData();
                DailyScheduleAssignment[] vDailyScheduleAssignmentDatas = ExportDailyScheduleAssignmentData1.GetDailyScheduleAssignmentData();

                // Récupérer tous les identifiants d'employés attendus
                var expectedEmployeeIds = vEmployeeProfessionalDatas.Select(e => e.employeeIdentificationNumber)
                    .Union(vDailyScheduleAssignmentDatas.Select(a => a.employeeIdentificationNumber))
                    .Distinct();

                // Parcourir tous les employés attendus
                foreach (var employeeId in expectedEmployeeIds)
                {
                    // Rechercher les données de pointage pour cet employé
                    var employeeClockings = vClockingList.Where(c => c.employeeIdentificationNumber == employeeId).ToList();

                    // Vérifier s'il y a des données de pointage pour cet employé
                    if (employeeClockings.Count > 0)
                    {
                        // Organiser les données de pointage par employé et par jour
                        var groupedClockings = employeeClockings.GroupBy(c => new { c.employeeIdentificationNumber, c.date });

                        // Parcourir les données organisées
                        foreach (var group in groupedClockings)
                        {
                            var currentDate = group.Key.date;

                            // Filtrer les entrées et les sorties pour cette journée
                            var entries = group.Where(c => c.inOutIndicator == 1).OrderBy(c => c.time).ToList();
                            var exits = group.Where(c => c.inOutIndicator == 2).OrderBy(c => c.time).ToList();

                            // Afficher toutes les entrées pour cette journée
                            Console.WriteLine($"Entrées pour l'employé avec matricule {employeeId} le {currentDate?.ToShortDateString()}:");
                            foreach (var entry in entries)
                            {
                                Console.WriteLine($"Heure d'entrée: {entry.time?.ToString("HH:mm:ss")}");
                            }

                            // Afficher toutes les sorties pour cette journée
                            Console.WriteLine($"Sorties pour l'employé avec matricule {employeeId} le {currentDate?.ToShortDateString()}:");
                            foreach (var exit in exits)
                            {
                                Console.WriteLine($"Heure de sortie: {exit.time?.ToString("HH:mm:ss")}");
                            }

                            // Calculer le temps total passé au travail pour cette journée
                            TimeSpan totalWorkTime = CalculateTotalWorkTime(entries, exits);

                            // Arrondir le temps total passé au travail à deux chiffres après la virgule
                            double totalWorkTimeInHours = Math.Round(totalWorkTime.TotalHours, 2);

                            // Récupérer la première entrée et la dernière sortie pour afficher les heures
                            var firstEntry = entries.FirstOrDefault();
                            var lastExit = exits.LastOrDefault();

                            // Trouver les données professionnelles correspondantes
                            var employeeData = vEmployeeProfessionalDatas.FirstOrDefault(e => e.employeeIdentificationNumber == employeeId);

                            // Trouver l'affectation horaire correspondante
                            var dailyScheduleAssignment = vDailyScheduleAssignmentDatas.FirstOrDefault(a => a.employeeIdentificationNumber == employeeId);

                            // Affichage des données de pointage
                            Console.WriteLine($"Matricule: {employeeId}");
                            Console.WriteLine($"Nom: {firstEntry?.employeeSurname}");
                            Console.WriteLine($"Prénom: {firstEntry?.employeeFirstName}");
                            Console.WriteLine($"Date: {currentDate?.ToShortDateString()}");
                            Console.WriteLine($"Heure d'entrée: {firstEntry?.time?.ToString("HH:mm:ss")}");
                            Console.WriteLine($"Heure de sortie: {lastExit?.time?.ToString("HH:mm:ss")}");
                            Console.WriteLine($"Temps total passé au travail: {totalWorkTimeInHours} heures");

                            // Afficher le libellé effectif
                            if (employeeData != null)
                            {
                                Console.WriteLine($"Section: {employeeData.currentSectionFullDescription}");
                                Console.WriteLine($"Libellé: {employeeData.currentSectionDescription}");
                            }
                            else
                            {
                                Console.WriteLine("Informations professionnelles non trouvées pour ce matricule");
                            }

                            if (dailyScheduleAssignment != null)
                            {
                                Console.WriteLine($"Théorique: {dailyScheduleAssignment.contractedTime}");
                            }
                            else
                            {
                                Console.WriteLine("Théorique: 0");
                            }

                            Console.WriteLine(); // Pour une meilleure lisibilité entre chaque enregistrement
                        }
                    }
                    else
                    {
                        // Si aucune donnée de pointage n'est trouvée pour cet employé
                        var employeeData = vEmployeeProfessionalDatas.FirstOrDefault(e => e.employeeIdentificationNumber == employeeId);
                        if (employeeData != null)
                        {
                            Console.WriteLine($"Aucune donnée de pointage trouvée pour l'employé:");
                            Console.WriteLine($"Matricule: {employeeId}");
                            Console.WriteLine($"Nom: {employeeData.employeeSurname}");
                            Console.WriteLine($"Prénom: {employeeData.employeeFirstName}");
                        }
                        else
                        {
                            Console.WriteLine($"Aucune donnée de pointage trouvée pour l'employé avec le matricule: {employeeId}");
                        }
                        Console.WriteLine(); // Pour une meilleure lisibilité entre chaque enregistrement
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite: {ex.Message}");
            }
        }

        // Méthode pour calculer le temps total passé au travail pour une journée donnée
        private static TimeSpan CalculateTotalWorkTime(List<Clocking> entries, List<Clocking> exits)
        {
            TimeSpan totalWorkTime = TimeSpan.Zero;

            for (int i = 0; i < Math.Min(entries.Count, exits.Count); i++)
            {
                if (entries[i].time.HasValue && exits[i].time.HasValue)
                {
                    TimeSpan workTime = exits[i].time.Value - entries[i].time.Value;
                    totalWorkTime += workTime;
                }
            }

            return totalWorkTime;
        }
    }
}
