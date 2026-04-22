using AnimalShelter;

namespace AnimalShelter
{
    public class AnimalFileManager
        {
            private const string FilePath = "animal-data.txt";

            public List<Animal> LoadAnimals()
                {
                    if (!File.Exists(FilePath))
                        return new List<Animal>();

                    return File.ReadAllLines(FilePath)
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .Select(Animal.FromString)
                            .ToList();
                }

            public void SaveAnimals(List<Animal> animals)
                {
                    File.WriteAllLines(FilePath, animals.Select(a => a.ToString()));
                }

            public void AddAnimal(Animal a)
                {
                    File.AppendAllText(FilePath, a.ToString() + Environment.NewLine);
                }


        }
}