namespace AnimalShelter;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please select mode (animal OR schedule): ");
        string mode = Console.ReadLine();
        if(mode=="animal") {
            
            string command;
            do {
                Console.Write("Enter animal name: ");
                string animalName = Console.ReadLine();
                
                Console.Write("Enter animal health information: ");
                string health = Console.ReadLine();
                
                File.AppendAllText("animal-data.txt",animalName+":"+health+Environment.NewLine);
                
                Console.Write("Enter command (end OR continue): ");
                command = Console.ReadLine();
            } while(command!="end");
        }
    }
}
