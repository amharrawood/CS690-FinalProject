using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AnimalShelter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nPlease select mode: \n");
            Console.WriteLine("\n1. Create or Update Animal Records");
            Console.WriteLine("\n2. Manage Veterinary Appointments");
            Console.Write("\nEnter choice (1 or 2): ");

            string mainMode = ReadNonEmpty();

            if (mainMode == "1")
            {
                string command;

                do
                {
                    Console.Write("\n-------------------------------\n");
                    Console.Write("\nCreate new or update existing?: \n");
                    Console.WriteLine("\n1. Create New Animal Records");
                    Console.WriteLine("\n2. Update Existing Animal Records");
                    Console.Write("\nEnter choice (1 or 2): ");

                    string action = ReadNonEmpty();

                    if (action == "1")
                    {
                        CreateAnimal();
                    }
                    else if (action == "2")
                    {
                        UpdateAnimal();
                    }

                    Console.Write("\n-------------------------------\n");
                    Console.Write("\nWhat's next? \n");
                    Console.WriteLine("\n1. Continue creating or updating records");
                    Console.WriteLine("\n2. Exit the program");
                    Console.Write("\nEnter choice (1 or 2): ");

                    command = ReadNonEmpty();

                } while (command != "2");
            }

            else if (mainMode == "2")
                {

                     string command;

                    do
                    {
                        Console.WriteLine("\n-------------------------------");
                        Console.WriteLine("Veterinary Appointment Management");
                        Console.WriteLine("1. Create Appointment");
                        Console.WriteLine("2. Update Appointment");
                        Console.WriteLine("3. View Appointments");
                        Console.WriteLine("4. Return to Main Menu");
                        Console.Write("Enter choice (1-4): ");

                        string choice = ReadNonEmpty();

                        switch (choice)
                        {
                            case "1":
                                CreateAppointment();
                                break;
                            case "2":
                                UpdateAppointment();
                                break;
                            case "3":
                                ViewAppointments();
                                break;
                            case "4":
                                return;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }

                        Console.WriteLine("\n1. Continue managing appointments");
                        Console.WriteLine("2. Return to main menu");
                        Console.Write("Enter choice: ");
                        command = ReadNonEmpty();

                    } while (command != "2");
                }


                }
       
        }

        // ---------------- Create animal records (UC1: FR1-5)--------------
        static void CreateAnimal()
        {
            Console.Write("\n-------------------------------\n");
            Console.Write("Enter animal name: ");
            string name = ReadNonEmpty();

            string species = GetValidatedChoice(
                "Enter animal species information (dog/cat): ",
                new[] { "dog", "cat" }
            );

            string sex = GetValidatedChoice(
                "Enter animal sex (male/female): ",
                new[] { "male", "female" }
            );

            Console.Write("Enter animal age information: ");
            string age = ReadNonEmpty();

            string vaccine = GetValidatedChoice(
                "Enter animal vaccine information (complete/incomplete): ",
                new[] { "complete", "incomplete" }
            );

            string repro = GetValidatedChoice(
                "Enter animal spay/neuter information (complete/incomplete): ",
                new[] { "complete", "incomplete" }
            );

            string status = GetValidatedChoice(
                "Enter animal status (active/ready/adopted): ",
                new[] { "active", "ready", "adopted" }
            );

            Console.Write("Enter other animal health history: ");
            string health = ReadNonEmpty();

            File.AppendAllText("animal-data.txt",
                $"{name}:{species}:{sex}:{age}:{vaccine}:{repro}:{status}:{health}{Environment.NewLine}");

            Console.WriteLine("Animal added.");
        }

        // ---------------- Update existing records (UC1:FR6----------------
        static void UpdateAnimal()
        {
            if (!File.Exists("animal-data.txt"))
            {
                Console.WriteLine("No animal records found.");
                return;
            }

            List<string> lines = File.ReadAllLines("animal-data.txt").ToList();

            Console.Write("Enter the name of the animal to update: ");
            string name = ReadNonEmpty();

            int index = lines.FindIndex(l => l.StartsWith(name + ":", StringComparison.OrdinalIgnoreCase));

            if (index == -1)
            {
                Console.WriteLine("Animal not found.");
                return;
            }

            string[] parts = lines[index].Split(':');

            string choice;
            do
            {
                Console.WriteLine("\nWhich field do you want to update?");
                Console.WriteLine("1. Species");
                Console.WriteLine("2. Sex");
                Console.WriteLine("3. Age");
                Console.WriteLine("4. Vaccine status");
                Console.WriteLine("5. Spay/neuter status");
                Console.WriteLine("6. Status");
                Console.WriteLine("7. Health History");
                Console.WriteLine("8. Done");
                Console.Write("Enter choice (1-8): ");

                choice = ReadNonEmpty();

                switch (choice)
                {
                    case "1":
                        parts[1] = GetValidatedChoice(
                            "Enter new species (dog/cat): ",
                            new[] { "dog", "cat" }
                        );
                        break;

                    case "2":
                        parts[2] = GetValidatedChoice(
                            "Enter new sex (male/female): ",
                            new[] { "male", "female" }
                        );
                        break;

                    case "3":
                        Console.Write("Enter new age: ");
                        parts[3] = ReadNonEmpty();
                        break;

                    case "4":
                        parts[4] = GetValidatedChoice(
                            "Enter new vaccine status (complete/incomplete): ",
                            new[] { "complete", "incomplete" }
                        );
                        break;

                    case "5":
                        parts[5] = GetValidatedChoice(
                            "Enter new spay/neuter status (complete/incomplete): ",
                            new[] { "complete", "incomplete" }
                        );
                        break;

                    case "6":
                        parts[6] = GetValidatedChoice(
                            "Enter new status (active/ready/adopted): ",
                            new[] { "active", "ready", "adopted" }
                        );
                        break;

                    case "7":
                        Console.Write("Enter new health history: ");
                        parts[7] = ReadNonEmpty();
                        break;

                    case "8":
                        Console.WriteLine("Saving changes...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice != "8");

            lines[index] = string.Join(":", parts);
            File.WriteAllLines("animal-data.txt", lines);

            Console.WriteLine("Animal updated successfully.");
            Console.Write("\n-------------------------------\n");
        }

        // -------------- Validation to help with testing accuracy ----------------
        static string GetValidatedChoice(string prompt, string[] validOptions)
        {
            string input;

            do
            {
                Console.Write(prompt);
                input = ReadNonEmpty().Trim().ToLower();

                if (!validOptions.Contains(input))
                {
                    Console.WriteLine("Invalid input. Valid options are: " + string.Join(", ", validOptions));
                }

            } while (!validOptions.Contains(input));

            return input;
        }

        static string ReadNonEmpty()
        {
            string? input;

            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input cannot be empty. Please try again.");
            }
            while (string.IsNullOrWhiteSpace(input));

            return input;
        }
    }

        static void CreateAppointment()
{
    Console.WriteLine("\n--- Create Appointment ---");

    Console.Write("Enter animal name: ");
    string name = ReadNonEmpty();

    Console.Write("Enter appointment date (YYYY-MM-DD): ");
    string date = ReadNonEmpty();

    Console.Write("Enter appointment time (HH:MM): ");
    string time = ReadNonEmpty();

    string type = GetValidatedChoice(
        "Enter appointment type (vaccine/checkup/surgery): ",
        new[] { "vaccine", "checkup", "surgery" }
    );

    Console.Write("Enter notes: ");
    string notes = ReadNonEmpty();

    File.AppendAllText("appointments.txt",
        $"{name}:{date}:{time}:{type}:{notes}{Environment.NewLine}");

    Console.WriteLine("Appointment created.");
}

static void UpdateAppointment()
{
    if (!File.Exists("appointments.txt"))
    {
        Console.WriteLine("No appointments found.");
        return;
    }

    List<string> lines = File.ReadAllLines("appointments.txt").ToList();

    Console.Write("Enter the animal name for the appointment: ");
    string name = ReadNonEmpty();

    int index = lines.FindIndex(l => l.StartsWith(name + ":", StringComparison.OrdinalIgnoreCase));

    if (index == -1)
    {
        Console.WriteLine("Appointment not found.");
        return;
    }

    string[] parts = lines[index].Split(':');

    string choice;
    do
    {
        Console.WriteLine("\nWhich field do you want to update?");
        Console.WriteLine("1. Date");
        Console.WriteLine("2. Time");
        Console.WriteLine("3. Type");
        Console.WriteLine("4. Notes");
        Console.WriteLine("5. Done");
        Console.Write("Enter choice (1-5): ");

        choice = ReadNonEmpty();

        switch (choice)
        {
            case "1":
                Console.Write("Enter new date (YYYY-MM-DD): ");
                parts[1] = ReadNonEmpty();
                break;

            case "2":
                Console.Write("Enter new time (HH:MM): ");
                parts[2] = ReadNonEmpty();
                break;

            case "3":
                parts[3] = GetValidatedChoice(
                    "Enter new type (vaccine/checkup/surgery): ",
                    new[] { "vaccine", "checkup", "surgery" }
                );
                break;

            case "4":
                Console.Write("Enter new notes: ");
                parts[4] = ReadNonEmpty();
                break;

            case "5":
                Console.WriteLine("Saving changes...");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

    } while (choice != "5");

    lines[index] = string.Join(":", parts);
    File.WriteAllLines("appointments.txt", lines);

    Console.WriteLine("Appointment updated successfully.");
}


