namespace AnimalShelter;

using Spectre.Console;

//class for UI related methods
public class ConsoleUI {

    
    
    public void show() {

            while (true)
            {
                Console.WriteLine("\n1. Manage Animals");
                Console.WriteLine("2. Manage Appointments");
                Console.WriteLine("3. Access Reports");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AnimalMenu();
                        break;
                    case "2":
                        AppointmentMenu();
                        break;
                    case "3":
                        ReportingMenu();
                        break;
                    case "4":
                        return;
                }
            }
        }

        public static void AnimalMenu()
        {
            Animal animal;
            animal = new Animal();
            
            Console.WriteLine("\n1. Create Animal");
            Console.WriteLine("2. Update Animal");
            Console.WriteLine("3. Back");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            if (choice == "1") animal.CreateAnimal();
            if (choice == "2") animal.UpdateAnimal();
        }

        public static void AppointmentMenu()
        {
            Appointment appointment;
            appointment = new Appointment();

            Console.WriteLine("\n1. Create Appointment");
            Console.WriteLine("2. Update Appointment");
            Console.WriteLine("3. View Appointments");
            Console.WriteLine("4. Back");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            if (choice == "1") appointment.CreateAppointment();
            if (choice == "2") appointment.UpdateAppointment();
            if (choice == "3") appointment.ViewAppointments();
        }

        public static void ReportingMenu()
        {
            ReportManager reportManager;
            reportManager = new ReportManager();
            
            Console.WriteLine("\nReporting Menu");
            Console.WriteLine("1. Animals Ready for Adoption");
            Console.WriteLine("2. Animals Needing Vaccines");
            Console.WriteLine("3. Upcoming Appointments");
            Console.WriteLine("4. Back");
            Console.Write("Enter choice: ");

            string choice = reportManager.ReadNonEmpty();

            switch (choice)
            {
                case "1":

                Console.WriteLine("\nFilter by species:");
                    Console.WriteLine("1. Dog");
                    Console.WriteLine("2. Cat");
                    Console.WriteLine("3. Both");
                    Console.Write("Enter choice: ");

                    string speciesChoice = reportManager.ReadNonEmpty();

                    string species = speciesChoice switch
                    {
                        "1" => "dog",
                        "2" => "cat",
                        _ => "both"
                    };
                    reportManager.ReportAnimalsAdoptable(species);
                    break;
                    
                    case "2":

                    Console.WriteLine("\nFilter by species:");
                    Console.WriteLine("1. Dog");
                    Console.WriteLine("2. Cat");
                    Console.WriteLine("3. Both");
                    Console.Write("Enter choice: ");

                    string speciesChoice2 = reportManager.ReadNonEmpty();

                    string species2 = speciesChoice2 switch
                    {
                        "1" => "dog",
                        "2" => "cat",
                        _ => "both"
                    };
                    reportManager.ReportAnimalsNeedingVaccines(species2);
        
                    
                    break;

                case "3":
                    reportManager.ReportAppointmentsByDateRangeAndSpecies();
                    break;
            }
        }   
   
    }
