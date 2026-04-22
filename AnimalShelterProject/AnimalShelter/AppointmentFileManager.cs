using AnimalShelter;

namespace AnimalShelter
{
    public class AppointmentFileManager
    {
        private const string FilePath = "appointments.txt";

        public List<Appointment> LoadAppointments()
            {
                if (!File.Exists(FilePath))
                    return new List<Appointment>();

                return File.ReadAllLines(FilePath)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .Select(Appointment.FromString)
                        .ToList();
            }

        public void SaveAppointments(List<Appointment> appts)
            {
                File.WriteAllLines(FilePath, appts.Select(a => a.ToString()));
            }

        public void AddAppointment(Appointment a)
            {
                File.AppendAllText(FilePath, a.ToString() + Environment.NewLine);
            }


    }
}