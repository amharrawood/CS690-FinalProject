
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AnimalShelter


{
    class Program
    {


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Manage Animals");
                Console.WriteLine("2. Manage Appointments");
                Console.WriteLine("3. Exit");
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

    }
}