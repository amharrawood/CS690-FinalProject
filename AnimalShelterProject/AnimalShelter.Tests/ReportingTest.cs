using Xunit;
using System.IO;
using System;
using System.Collections.Generic;

namespace AnimalShelter.Tests
{
    public class ReportManagerTest
    {
        //Test that the appointment data for the Upcoming Appointment Report loads correctly
        
        [Fact]
        public void LoadAppointments_ReturnsCorrectAppointments()
        {
            // Arrange: create temp directory
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            string originalDir = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(tempDir);

                // Create fake appointments file
                File.WriteAllLines("appointments.txt", new[]
                {
                    "Buddy|2025-05-01|10:00|Checkup|Dr. Smith",
                    "Mittens|2025-05-02|11:00|Vaccine|Dr. Jones"
                });

                var fileManager = new AppointmentFileManager();

                // Act
                var appts = fileManager.LoadAppointments();

                // Assert
                Assert.Equal(2, appts.Count);

                Assert.Equal("Buddy", appts[0].AnimalName);
                Assert.Equal(new DateTime(2025, 5, 1), appts[0].Date);
                Assert.Equal("10:00", appts[0].Time);
                Assert.Equal("Checkup", appts[0].Type);
                Assert.Equal("Dr. Smith", appts[0].Notes);

                Assert.Equal("Mittens", appts[1].AnimalName);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDir);
                Directory.Delete(tempDir, true);
            }
        }

    }    
}