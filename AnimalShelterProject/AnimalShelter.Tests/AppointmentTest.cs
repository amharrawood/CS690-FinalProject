using Xunit;

namespace AnimalShelter.Tests
{
    public class AppointmentTest
    {
        [Fact]
        public void FromString_ParsesCorrectly()
        {
            string line = "Buddy|2025-05-01|10:00|Checkup|Dr. Smith";

            var appt = Appointment.FromString(line);

            Assert.Equal("Buddy", appt.AnimalName);
            Assert.Equal(new DateTime(2025, 5, 1), appt.Date);
            Assert.Equal("10:00", appt.Time);
            Assert.Equal("Checkup", appt.Type);
            Assert.Equal("Dr. Smith", appt.Notes);
        }
    }
}
