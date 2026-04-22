namespace AnimalShelter;
using Spectre.Console;




    //--------Animal Class---------------
    
    public class Animal
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string VaccineStatus { get; set; }
        public string ReproStatus { get; set; }
        public string Status { get; set; }
        public string HealthHistory { get; set; }
        public string Behavior { get; set; }

        public Animal()
                {
                    Name = "";
                    Species = "";
                    Sex = "";
                    Age = "";
                    VaccineStatus = "";
                    ReproStatus = "";
                    Status = "";
                    HealthHistory = "";
                    Behavior = "";
                }
                    
        
        
        public override string ToString()
        {
            return $"{Name}:{Species}:{Sex}:{Age}:{VaccineStatus}:{ReproStatus}:{Status}:{HealthHistory}:{Behavior}";
        }

        public static Animal FromString(string line)
        {
            var p = line.Split(':');
            return new Animal
            {
                Name = p[0],
                Species = p[1],
                Sex = p[2],
                Age = p[3],
                VaccineStatus = p[4],
                ReproStatus = p[5],
                Status = p[6],
                HealthHistory = p[7],
                Behavior = p[8]
            };
        }
    

        public void CreateAnimal()
            {    
                var animalFileManager = new AnimalFileManager();

                var name = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter name:")
                        .Validate(n => 
                            string.IsNullOrWhiteSpace(n)
                              ? ValidationResult.Error("[red]Name cannot be empty[/]")
                                        : ValidationResult.Success()));

                        var species = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Select species")
                                .AddChoices("dog", "cat"));

                        var sex = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Select sex")
                                .AddChoices("male", "female"));

                        var age = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter age:")
                                .Validate(a =>
                                    string.IsNullOrWhiteSpace(a)
                                        ? ValidationResult.Error("[red]Age cannot be empty[/]")
                                        : ValidationResult.Success()));

                        var vaccine = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Select vaccine status")
                                .AddChoices("complete", "incomplete"));

                        var repro = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Select spay/neuter status")
                                .AddChoices("complete", "incomplete"));

                        var status = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Select shelter status")
                                .AddChoices("active", "ready", "adopted"));

                        var health = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter health history:")
                                .Validate(h =>
                                    string.IsNullOrWhiteSpace(h)
                                        ? ValidationResult.Error("[red]Health history cannot be empty[/]")
                                        : ValidationResult.Success()));

                        var behavior = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter behavior and personality:")
                                .Validate(b =>
                                    string.IsNullOrWhiteSpace(b)
                                        ? ValidationResult.Error("[red]Behavior cannot be empty[/]")
                                        : ValidationResult.Success()));

                        var animal = new Animal
                        {
                            Name = name,
                            Species = species,
                            Sex = sex,
                            Age = age,
                            VaccineStatus = vaccine,
                            ReproStatus = repro,
                            Status = status,
                            HealthHistory = health,
                            Behavior = behavior
                        };

                        animalFileManager.AddAnimal(animal);

                        AnsiConsole.MarkupLine("[green]Animal added successfully![/]");
                }


        public void UpdateAnimal()
                {
                    var animalFileManager = new AnimalFileManager();
                    var animals = animalFileManager.LoadAnimals();

                    if (animals.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]No animals found to update.[/]");
                        return;
                    }

                    var selectedName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Select an animal to update[/]")
                            .PageSize(10)
                            .AddChoices(animals.Select(a => a.Name)));

                    var animal = animals.First(a => a.Name == selectedName);

                    bool done = false;

                    while (!done)
                    {
                        var field = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title($"Updating [green]{animal.Name}[/]. Choose a field:")
                                .AddChoices(
                                    "Species",
                                    "Sex",
                                    "Age",
                                    "Vaccine Status",
                                    "Spay/Neuter Status",
                                    "Shelter Status",
                                    "Health History",
                                    "Behavior and Personality",
                                    "Done"));

                        switch (field)
                        {
                            case "Species":
                                animal.Species = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Select species")
                                        .AddChoices("dog", "cat"));
                                break;

                            case "Sex":
                                animal.Sex = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Select sex")
                                        .AddChoices("male", "female"));
                                break;

                            case "Age":
                                animal.Age = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Enter new age:")
                                        .Validate(a =>
                                            string.IsNullOrWhiteSpace(a)
                                                ? ValidationResult.Error("[red]Age cannot be empty[/]")
                                                : ValidationResult.Success()));
                                break;

                            case "Vaccine Status":
                                animal.VaccineStatus = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Select vaccine status")
                                        .AddChoices("complete", "incomplete"));
                                break;

                            case "Spay/Neuter Status":
                                animal.ReproStatus = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Select spay/neuter status")
                                        .AddChoices("complete", "incomplete"));
                                break;

                            case "Shelter Status":
                                animal.Status = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Select shelter status")
                                        .AddChoices("active", "ready", "adopted"));
                                break;

                            case "Health History":
                                animal.HealthHistory = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Enter updated health history:")
                                        .Validate(h =>
                                            string.IsNullOrWhiteSpace(h)
                                                ? ValidationResult.Error("[red]Health history cannot be empty[/]")
                                                : ValidationResult.Success()));
                                break;

                            case "Behavior and Personality":
                                animal.Behavior = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Enter updated behavior/personality:")
                                        .Validate(b =>
                                            string.IsNullOrWhiteSpace(b)
                                                ? ValidationResult.Error("[red]Behavior cannot be empty[/]")
                                                : ValidationResult.Success()));
                                break;

                            case "Done":
                                done = true;
                                break;
                        }
                    }

                    animalFileManager.SaveAnimals(animals);
                    AnsiConsole.MarkupLine("[green]Animal updated successfully![/]");
                }

       
        
    }
