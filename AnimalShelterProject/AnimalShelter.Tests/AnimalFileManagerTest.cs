using Xunit;
using System.IO;

namespace AnimalShelter.Tests
{
    public class AnimalFileTest
    {
        [Fact]
        public void AddAnimal_WritesCorrectFormat_UsingTempDirectory()
        {
            // Create a temporary directory
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            // Save original working directory
            string originalDir = Directory.GetCurrentDirectory();

            try
            {
                // Switch to temp directory
                Directory.SetCurrentDirectory(tempDir);

                var manager = new AnimalFileManager();

                var animal = new Animal
                {
                    Name = "Buddy",
                    Species = "dog",
                    Sex = "male",
                    Age = "3",
                    VaccineStatus = "complete",
                    ReproStatus = "complete",
                    Status = "active",
                    HealthHistory = "healthy",
                    Behavior = "friendly"
                };

                // Act
                manager.AddAnimal(animal);

                // Assert
                string filePath = Path.Combine(tempDir, "animal-data.txt");
                Assert.True(File.Exists(filePath));

                var lines = File.ReadAllLines(filePath);

                Assert.Single(lines);
                Assert.Equal(
                    "Buddy:dog:male:3:complete:complete:active:healthy:friendly",
                    lines[0]);
            }
            finally
            {
                // Restore working directory
                Directory.SetCurrentDirectory(originalDir);

                // Clean up
                Directory.Delete(tempDir, true);
            }
        }
    }
}
