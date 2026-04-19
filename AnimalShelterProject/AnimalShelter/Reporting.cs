using System;
using System.Linq;
using System.Collections.Generic;

namespace AnimalShelter
{
    public class ReportManager
    {
        private readonly AnimalFileManager animalFileManager = new AnimalFileManager();
        private readonly AppointmentFileManager appointmentFileManager = new AppointmentFileManager();

        //Species Filter
        
        private List<Animal> FilterBySpecies(List<Animal> animals, string species)
        {
            species = species.ToLower();

            return species switch
            {
                "dog" => animals.Where(a => a.Species == "dog").ToList(),
                "cat" => animals.Where(a => a.Species == "cat").ToList(),
                _ => animals // "both" or anything else returns all
            };
        }

        //Date Range Adjustment

        private bool TryParseDate(string input, out DateTime date)
        {
            return DateTime.TryParse(input, out date);
        }              

            private List<Appointment> FilterAppointmentsBySpecies(List<Appointment> appts, string species)
            {
                var animals = animalFileManager.LoadAnimals();

                // Build a lookup: animal name → species
                var speciesLookup = animals.ToDictionary(a => a.Name.ToLower(), a => a.Species.ToLower());

                species = species.ToLower();

                return appts.Where(a =>
                {
                    if (!speciesLookup.TryGetValue(a.AnimalName.ToLower(), out var animalSpecies))
                        return false; // appointment for unknown animal

                    return species switch
                    {
                        "dog" => animalSpecies == "dog",
                        "cat" => animalSpecies == "cat",
                        _ => true // both
                    };
                }).ToList();
            }



        private List<Appointment> FilterByDateRange(List<Appointment> appts, DateTime start, DateTime end)
        {
            return appts
                .Where(a => DateTime.TryParse(a.Date, out var d) && d >= start && d <= end)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToList();
        }            



        // Animal Reports

        public void ReportAnimalsByStatus(string species)
        {
            var animals = animalFileManager.LoadAnimals();
            animals = FilterBySpecies(animals, species);

            if (animals.Count == 0)
            {
                Console.WriteLine("No animals found for this species filter.");
                return;
            }

            var grouped = animals.GroupBy(a => a.Status);

            Console.WriteLine($"\nAnimals by Status ({species}):");
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }
        }

        public void ReportAnimalsAdoptable(string species)
        {
            var animals = animalFileManager.LoadAnimals();
            animals = FilterBySpecies(animals, species);

            var adoptable = animals.Where(a => a.Status == "ready").ToList();

            Console.WriteLine("\nAnimals Ready to Adopt:");
            if (adoptable.Count == 0)
            {
                Console.WriteLine("No animals ready to adopt at this time.");
                return;
            }

            foreach (var a in adoptable)
            {
                Console.WriteLine($"{a.Name} ({a.Species})");
            }
        }

        public void ReportAnimalsNeedingVaccines(string species)
        {
            var animals = animalFileManager.LoadAnimals();
            animals = FilterBySpecies(animals, species);

            var needing = animals.Where(a => a.VaccineStatus == "incomplete").ToList();

            Console.WriteLine($"\nAnimals Needing Vaccines ({species}):");

            if (needing.Count == 0)
            {
                Console.WriteLine("None.");
                return;
            }

            foreach (var a in needing)
                Console.WriteLine($"{a.Name} ({a.Species})");
        }

        // Appointment report
   
         public void ReportAppointmentsByDateRangeAndSpecies()
            {
                Console.Write("Enter start date (YYYY-MM-DD): ");
                string startInput = ReadNonEmpty();

                Console.Write("Enter end date (YYYY-MM-DD): ");
                string endInput = ReadNonEmpty();

                if (!TryParseDate(startInput, out var start) ||
                    !TryParseDate(endInput, out var end))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                if (end < start)
                {
                    Console.WriteLine("End date must be after start date.");
                    return;
                }

                Console.WriteLine("\nFilter by species:");
                Console.WriteLine("1. Dog");
                Console.WriteLine("2. Cat");
                Console.WriteLine("3. Both");
                Console.Write("Enter choice: ");

                string speciesChoice = ReadNonEmpty();

                string species = speciesChoice switch
                {
                    "1" => "dog",
                    "2" => "cat",
                    _ => "both"
                };

                var appts = appointmentFileManager.LoadAppointments();

                // Apply both filters
                appts = FilterByDateRange(appts, start, end);
                appts = FilterAppointmentsBySpecies(appts, species);

                Console.WriteLine($"\nAppointments ({species}) from {start:yyyy-MM-dd} to {end:yyyy-MM-dd}:");

                if (appts.Count == 0)
                {
                    Console.WriteLine("No appointments found for this filter.");
                    return;
                }

                foreach (var a in appts)
                {
                    Console.WriteLine($"{a.Date} {a.Time} - {a.AnimalName} ({a.Type})");
                }
            }
    
    
    }
}