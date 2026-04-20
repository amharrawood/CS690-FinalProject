using System;
using System.Linq;
using System.Collections.Generic;
using Spectre.Console;

namespace AnimalShelter
{
    public class ReportManager
    {
        private readonly AnimalFileManager animalFileManager = new AnimalFileManager();
        private readonly AppointmentFileManager appointmentFileManager = new AppointmentFileManager();

        //Species Filter Assiting List
        
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

        public void ReportAnimalsAdoptable()
        {
            
            string species = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[yellow]Filter by species[/]")
                                .AddChoices("dog", "cat", "both"));
            
            
            var animals = animalFileManager.LoadAnimals();
            animals = FilterBySpecies(animals, species);

            

            var adoptable = animals.Where(a => a.Status == "ready").ToList();

            Console.WriteLine("\nAnimals Ready to Adopt:");
            if (adoptable.Count == 0)
            {
                Console.WriteLine("No animals ready to adopt at this time.");
                return;
            }

            AnsiConsole.Write(new Rule("[blue]Animals Ready to Adopt[/]").Centered());
            
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Name[/]")
                .AddColumn("[yellow]Species[/]");


            foreach (var a in animals)
                table.AddRow(a.Name, a.Species);

            AnsiConsole.Write(table);
        }

       
       
       public void ReportAnimalsNeedingVaccines()
        {
            
            string species = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Filter by species[/]")
                    .AddChoices("dog", "cat", "both"));
            
            
            var animals = animalFileManager.LoadAnimals()
                .Where(a => a.VaccineStatus == "incomplete")
                .ToList();

            if (animals.Count == 0)
            {
                AnsiConsole.MarkupLine("[green]All animals are vaccinated[/]");
                return;
            }
            
            AnsiConsole.Write(new Rule("[blue]Animals Needing Vaccines[/]").Centered());
            
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Name[/]")
                .AddColumn("[yellow]Species[/]");


            foreach (var a in animals)
                table.AddRow(a.Name, a.Species);

            AnsiConsole.Write(table);
        }

       
        // Appointment report
   
         public void ReportAppointmentsByDateRangeAndSpecies()
            {
                    AnsiConsole.Write(new Rule("[blue]Appointments by Date Range + Species[/]").Centered());

                        string start = AnsiConsole.Ask<string>("[yellow]Start date (YYYY-MM-DD):[/]");
                        string end = AnsiConsole.Ask<string>("[yellow]End date (YYYY-MM-DD):[/]");

                        DateTime startDate = DateTime.Parse(start);
                        DateTime endDate = DateTime.Parse(end);

                        string species = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[yellow]Filter by species[/]")
                                .AddChoices("dog", "cat", "both"));

                        var appts = appointmentFileManager.LoadAppointments();

                        // Filter by date
                        appts = appts
                            /*.Where(a => DateTime.TryParse(a.Date, out var d) && d >= startDate && d <= endDate)
                            .ToList();*/
                            .Where(a => a.Date >= startDate && a.Date <= endDate)
                            .ToList();

                        // Filter by species
                        var animals = animalFileManager.LoadAnimals();
                        var lookup = animals.ToDictionary(a => a.Name.ToLower(), a => a.Species.ToLower());

                        if (species != "both")
                        {
                            appts = appts
                                .Where(a => lookup.TryGetValue(a.AnimalName.ToLower(), out var s) && s == species)
                                .ToList();
                        }

                        if (appts.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]No appointments found for this filter[/]");
                            return;
                        }

                        var table = new Table()
                            .Border(TableBorder.Rounded)
                            .AddColumn("[yellow]Date[/]")
                            .AddColumn("[yellow]Time[/]")
                            .AddColumn("[yellow]Animal[/]")
                            .AddColumn("[yellow]Type[/]");

                        foreach (var a in appts)
                            table.AddRow(a.Date.ToString("MM/dd/yyyy"), a.Time, a.AnimalName, a.Type);

                        AnsiConsole.Write(table);
                    }



        public string ReadNonEmpty()
        {
            string? input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input cannot be empty.");
            }
            while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public string GetValidatedChoice(string prompt, string[] valid)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = ReadNonEmpty().ToLower();

                if (!valid.Contains(input))
                    Console.WriteLine("Invalid option.");
            }

            while (!valid.Contains(input));

            return input;
        }
    
       


    
    }
}