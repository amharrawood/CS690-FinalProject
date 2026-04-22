namespace AnimalShelter;
using Spectre.Console;

//appointment related classes and methods



    public class Appointment
    {
        public string AnimalName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }

        public Appointment()
            {
                AnimalName = "";
                Date = DateTime.MinValue;
                Time = "";
                Type = "";
                Notes = "";
            }


        public override string ToString()
            {
                return $"{AnimalName}|{Date}|{Time}|{Type}|{Notes}";
            }

        public static Appointment FromString(string line)
            {
                var p = line.Split('|');
                return new Appointment
                {
                    AnimalName = p[0],
                    Date = DateTime.TryParse(p[1], out var d) ? d : DateTime.MinValue,
                    Time = p[2],
                    Type = p[3],
                    Notes = p[4]
                };
            }
    

        public void CreateAppointment()
            {
                var appointmentFileManager = new AppointmentFileManager();
                var animalFileManager = new AnimalFileManager();

                // Load animals for dropdown
                var animals = animalFileManager.LoadAnimals();

                if (animals.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No animals found. Add animals before creating appointments.[/]");
                    return;
                }

                var animalName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Select an animal[/]")
                            .PageSize(10)
                            .AddChoices(animals.Select(a => a.Name)));


                    var date = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>("[yellow]Enter appointment date (YYYY/MM/DD):[/]")
                            .Validate(d =>
                            {
                                return d >= DateTime.Today
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]Date cannot be in the past[/]");
                            }));

                    // Time dropdown
                    var time = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Select appointment time[/]")
                            .AddChoices("08:00","09:00","10:00", "11:00", "12:00", "01:00", "02:00", "03:00", "04:00"));

                    // Type dropdown
                    var type = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Select appointment type[/]")
                            .AddChoices("Vaccine", "Checkup", "Surgery"));

                    // Notes 
                    var notes = AnsiConsole.Ask<string>("[yellow]Veterinarian and Locaton:[/]");

                    // Create appointment
                    var appt = new Appointment
                    {
                        AnimalName = animalName,
                        Date = date,
                        Time = time,
                        Type = type,
                        Notes = notes
                    };

                    appointmentFileManager.AddAppointment(appt);

                    AnsiConsole.MarkupLine("[green]Appointment created successfully![/]");
                }

        public void UpdateAppointment()
                {
                    
                    AppointmentFileManager appointmentFileManager;
                    appointmentFileManager = new AppointmentFileManager();
                    
                    var appts = appointmentFileManager.LoadAppointments();

                    if (appts.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No appointments found.[/]");
                    return;
                }

                var labels = appts
                    
                    .Select(a => $"{a.AnimalName} — {a.Date:MM/dd/yyyy} — {a.Time} — {a.Type}")
                    .ToList();

                var selectedLabel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Select an appointment to update[/]")
                        .PageSize(10)
                        .AddChoices(labels));

                var appt = appts[labels.IndexOf(selectedLabel)];

                bool done = false;

                while (!done)
                {
                    
                    var newfield = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"Updating appointment for [green]{appt.AnimalName}[/]")
                            .AddChoices("Date", "Time", "Type", "Notes", "Done"));

                    switch (newfield)
                    {
                        case "Date":
                            appt.Date = AnsiConsole.Prompt(
                                new TextPrompt<DateTime>("Enter new date (MM/DD/YYYY):")
                                .Validate(d =>
                                    {
                                        return d >= DateTime.Today
                                            ? ValidationResult.Success()
                                            : ValidationResult.Error("[red]Date cannot be in the past[/]");
                                    }));
                            break;

                        case "Time":
                            appt.Time = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("Select new time")
                                    .AddChoices("08:00","09:00","10:00", "11:00", "12:00", "01:00", "02:00", "03:00", "04:00"));
                            break;

                        case "Type":
                            appt.Type = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("Select appointment type")
                                    .AddChoices("Vaccine", "Checkup", "Surgery"));
                            break;

                        case "Notes":
                            appt.Notes = AnsiConsole.Ask<string>("Enter new Veterinarian and Location:");
                            break;

                        case "Done":
                            done = true;
                            break;
                    }


                }

                        appointmentFileManager.SaveAppointments(appts);
                        Console.WriteLine("Appointment updated.");
                        AnsiConsole.MarkupLine("[green]Appointment updated successfully![/]");
            }    

        public void ViewAppointments()
            {
                AppointmentFileManager appointmentFileManager;
                appointmentFileManager = new AppointmentFileManager();
                
                var appts = appointmentFileManager.LoadAppointments();

                if (appts.Count == 0)
                {
                    Console.WriteLine("No appointments found.");
                    return;
                }

                foreach (var a in appts)
                {
                    Console.WriteLine($"Animal: {a.AnimalName}, Date: {a.Date}, Time: {a.Time}, Type: {a.Type}, Notes: {a.Notes}");
                }
            }
            
   
    }
