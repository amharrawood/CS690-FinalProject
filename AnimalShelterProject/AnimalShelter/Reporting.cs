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
                    AnsiConsole.Write(new Rule("[blue]Upcoming Appointments by Date Range + Species[/]").Centered());


                        DateTime startDate;
                        DateTime endDate;

                        startDate = AnsiConsole.Prompt(
                                new TextPrompt<DateTime>("Enter new date (MM/DD/YYYY):")
                                    .Validate(d =>
                                    {
                                        return d >= DateTime.Today
                                            ? ValidationResult.Success()
                                            : ValidationResult.Error("[red]Date cannot be in the past[/]");
                                    }))
                                    ;
                        endDate = AnsiConsole.Prompt(
                                new TextPrompt<DateTime>("Enter new date (MM/DD/YYYY):")
                                    .Validate(d =>
                                    {
                                        return d >= DateTime.Today
                                            ? ValidationResult.Success()
                                            : ValidationResult.Error("[red]Date cannot be in the past[/]");
                                    }));

                        string species = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[yellow]Filter by species[/]")
                                .AddChoices("dog", "cat", "both"));

                        var appts = appointmentFileManager.LoadAppointments();

                        // Filter by date
                        appts = appts

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


    
    }
}