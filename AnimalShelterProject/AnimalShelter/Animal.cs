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
    
        // ---------------- Animal Methods ----------------
        public void CreateAnimal()
        {
            AnimalFileManager animalFileManager;
            animalFileManager = new AnimalFileManager();           
           
            Console.Write("Enter name: ");
            string name = ReadNonEmpty();

           string species = GetValidatedChoice("Species (dog/cat): ", new[] { "dog", "cat" });
            string sex = GetValidatedChoice("Sex (male/female): ", new[] { "male", "female" });

            Console.Write("Age: ");
            string age = ReadNonEmpty();

            string vaccine = GetValidatedChoice("Vaccine (complete/incomplete): ", new[] { "complete", "incomplete" });
            string repro = GetValidatedChoice("Spay/Neuter (complete/incomplete): ", new[] { "complete", "incomplete" });
            string status = GetValidatedChoice("Status (active/ready/adopted): ", new[] { "active", "ready", "adopted" });

            Console.Write("Health history: ");
            string health = ReadNonEmpty();

            Console.Write("Behavior and personality: ");
            string behavior = ReadNonEmpty();

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
            Console.WriteLine("Animal added.");
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

    // Build list of names for the dropdown
    var animalNames = animals.Select(a => a.Name).ToList();

    // Drop-down menu to select animal
    var selectedName = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[yellow]Select an animal to update[/]")
            .PageSize(10)
            .AddChoices(animalNames));

    var animal = animals.First(a => a.Name == selectedName);

    bool done = false;

    while (!done)
    {
        var fieldChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"Updating [green]{animal.Name}[/]. Choose a field:")
                .AddChoices(new[]
                {
                    "Species",
                    "Sex",
                    "Age",
                    "Vaccine Status",
                    "Spay/Neuter Status",
                    "Shelter Status",
                    "Health History",
                    "Behavior and Personality",
                    "Done"
                }));

        switch (fieldChoice)
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
                animal.Age = AnsiConsole.Ask<string>("Enter new age:");
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
                animal.HealthHistory = AnsiConsole.Ask<string>("Enter updated health history:");
                break;

            case "Behavior and Personality":
                animal.Behavior = AnsiConsole.Ask<string>("Enter updated behavior/personality:");
                break;

            case "Done":
                done = true;
                break;
        }
    }

    animalFileManager.SaveAnimals(animals);
    AnsiConsole.MarkupLine("[green]Animal updated successfully![/]");
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
