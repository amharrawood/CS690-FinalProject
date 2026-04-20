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
            AnimalFileManager animalFileManager;
            animalFileManager = new AnimalFileManager();           
                       
            Console.Write("Enter animal name: ");
            string name = ReadNonEmpty();

            var animals = animalFileManager.LoadAnimals();
            var animal = animals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (animal == null)
            {
                Console.WriteLine("Animal not found.");
                return;
            }

            string choice;
            do
            {
                Console.WriteLine("\n1. Species");
                Console.WriteLine("2. Sex");
                Console.WriteLine("3. Age");
                Console.WriteLine("4. Vaccine");
                Console.WriteLine("5. Spay/Neuter");
                Console.WriteLine("6. Status");
                Console.WriteLine("7. Health History");
                Console.WriteLine("8. Behavior and Personality");
                Console.WriteLine("9. Done");
                Console.Write("Enter choice: ");

                choice = ReadNonEmpty();

                switch (choice)
                {
                    case "1":
                        animal.Species = GetValidatedChoice("Species (dog/cat): ", new[] { "dog", "cat" });
                        break;
                    case "2":
                        animal.Sex = GetValidatedChoice("Sex (male/female): ", new[] { "male", "female" });
                        break;
                    case "3":
                        Console.Write("Age: ");
                        animal.Age = ReadNonEmpty();
                        break;
                    case "4":
                        animal.VaccineStatus = GetValidatedChoice("Vaccine (complete/incomplete): ", new[] { "complete", "incomplete" });
                        break;
                    case "5":
                        animal.ReproStatus = GetValidatedChoice("Spay/Neuter (complete/incomplete): ", new[] { "complete", "incomplete" });
                        break;
                    case "6":
                        animal.Status = GetValidatedChoice("Status (active/ready/adopted): ", new[] { "active", "ready", "adopted" });
                        break;
                    case "7":
                        Console.Write("Health History: ");
                        animal.HealthHistory = ReadNonEmpty();
                        break;
                    case "8":
                        Console.Write("Behavior and Personality: ");
                        animal.Behavior = ReadNonEmpty();
                        break;
                }

            } while (choice != "9");

            animalFileManager.SaveAnimals(animals);
            Console.WriteLine("Animal updated.");
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
