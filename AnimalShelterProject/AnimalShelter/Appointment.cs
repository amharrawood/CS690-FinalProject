namespace AnimalShelter

//appointment related classes and methods


{
    public class Appointment
    {
        public string AnimalName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }

        public Appointment()
        {
            AnimalName = "";
            Date = "";
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
                Date = p[1],
                Time = p[2],
                Type = p[3],
                Notes = p[4]
            };
        }
   
    public void CreateAppointment()
        {
           AppointmentFileManager appointmentFileManager;
            appointmentFileManager = new AppointmentFileManager();
           
            Console.Write("Animal name: ");
            string name = ReadNonEmpty();

            Console.Write("Date (YYYY-MM-DD): ");
            string date = ReadNonEmpty();

            Console.Write("Time (HH:MM): ");
            string time = ReadNonEmpty();

            string type = GetValidatedChoice("Type (vaccine/checkup/surgery): ", new[] { "vaccine", "checkup", "surgery" });

            Console.Write("Notes: ");
            string notes = ReadNonEmpty();

            var appt = new Appointment
            {
                AnimalName = name,
                Date = date,
                Time = time,
                Type = type,
                Notes = notes
            };

            appointmentFileManager.AddAppointment(appt);
            Console.WriteLine("Appointment created.");
        }

        public void UpdateAppointment()
        {
            
            AppointmentFileManager appointmentFileManager;
            appointmentFileManager = new AppointmentFileManager();
            
            Console.Write("Enter animal name: ");
            string name = ReadNonEmpty();

            var appts = appointmentFileManager.LoadAppointments();
            var appt = appts.FirstOrDefault(a => a.AnimalName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (appt == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            string choice;
            do
            {
                Console.WriteLine("\n1. Date");
                Console.WriteLine("2. Time");
                Console.WriteLine("3. Type");
                Console.WriteLine("4. Notes");
                Console.WriteLine("5. Done");
                Console.Write("Enter choice: ");

                choice = ReadNonEmpty();

                switch (choice)
                {
                    case "1":
                        Console.Write("Date: ");
                        appt.Date = ReadNonEmpty();
                        break;
                    case "2":
                        Console.Write("Time: ");
                        appt.Time = ReadNonEmpty();
                        break;
                    case "3":
                        appt.Type = GetValidatedChoice("Type (vaccine/checkup/surgery): ", new[] { "vaccine", "checkup", "surgery" });
                        break;
                    case "4":
                        Console.Write("Notes: ");
                        appt.Notes = ReadNonEmpty();
                        break;
                }

            } while (choice != "5");

            appointmentFileManager.SaveAppointments(appts);
            Console.WriteLine("Appointment updated.");
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