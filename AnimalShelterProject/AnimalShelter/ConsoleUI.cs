namespace AnimalShelter;

using Spectre.Console;

//class for UI related methods
public class ConsoleUI {

    
    
    public void show() {

           ShowStartupScreen();
           
            while (true)
            {
              
                    var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Main Menu[/]")
                        .HighlightStyle("cyan")
                        .AddChoices(new[]
                        {
                            "Manage Animals",
                            "Manage Appointments",
                            "Reporting",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Manage Animals":
                        AnimalMenu();
                        break;

                    case "Manage Appointments":
                        AppointmentMenu();
                        break;

                    case "Reporting":
                        ReportingMenu();
                        break;

                    case "Exit":
                        AnsiConsole.MarkupLine("[green]Goodbye![/]");
                        return;
                }
            }
        }

        
         static void ShowStartupScreen()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Clara's Animal Shelter")
                    .Centered()
                    .Color(Color.CadetBlue));

            AnsiConsole.Write(
                new Rule("[yellow]Management System[/]")
                    .RuleStyle("grey")
                    .Centered());

            AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
            AnsiConsole.Clear();
        }
        
        
        
        
        public static void AnimalMenu()
        {
            Animal animal;
            animal = new Animal();
            
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Animal Management[/]")
                    .AddChoices("Create Animal", "Update Animal", "Back"));

            if (choice == "Create Animal") animal.CreateAnimal();
            if (choice == "Update Animal") animal.UpdateAnimal();
            if (choice == "Back") return;
        }

        public static void AppointmentMenu()
        {
            Appointment appointment;
            appointment = new Appointment();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Appointment Management[/]")
                    .AddChoices("Create Appointment", "Update Appointment", "View Appointments", "Back"));

            if (choice == "Create Appointment") appointment.CreateAppointment();
            if (choice == "Update Appointment") appointment.UpdateAppointment();
            if (choice == "View Appointments") appointment.ViewAppointments();
            if (choice == "Back") return;
        }

        public static void ReportingMenu()
        {
            ReportManager reportManager;
            reportManager = new ReportManager();
            
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Reporting[/]")
                    .AddChoices(
                        "Animals Ready to Adopt",
                        "Animals Needing Vaccines",
                        "Appointments by Date Range + Species",
                        "Back"));

            switch (choice)
            {
            case "Animals Ready to Adopt":

                    reportManager.ReportAnimalsAdoptable();
                    break;
                    
            case "Animals Needing Vaccines":

                    reportManager.ReportAnimalsNeedingVaccines();                
                    break;

            case "Appointments by Date Range + Species":

                    reportManager.ReportAppointmentsByDateRangeAndSpecies();
                    break;

            case "Back": return;
            
                        
            }
        }   
   
    }
