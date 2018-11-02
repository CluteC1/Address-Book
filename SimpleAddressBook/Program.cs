using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAddressBook
{
    class Program
    {
        public static List<Person> People = new List<Person>();
        static void Main()
        {
            string command = "";
            while (command != "exit")
            {
                Console.Clear();
                Console.WriteLine("Press enter to see your commands!");
                Console.Write("Please enter a command: ");
                command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "add":
                        Console.ForegroundColor = ConsoleColor.Green;
                        AddPerson();
                        Console.ResetColor();
                        break;
                    case "remove":
                        Console.ForegroundColor = ConsoleColor.Red;
                        RemovePerson();
                        Console.ResetColor();
                        break;
                    case "list":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        ListPeople();
                        Console.ResetColor();
                        break;
                    case "search":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        SearchPerson();
                        Console.ResetColor();
                        break;
                    default:
                        // Dont display the help text if the user intends to exit the application
                        if (command != "exit")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            DisplayHelp();
                            Console.ResetColor();
                        }
                        break;
                }
            }
        }

        private static void DisplayHelp()
        {
            Console.Clear();
            Console.WriteLine("Available Commands:");
            Console.WriteLine("add\tAdds a person to address book");
            Console.WriteLine("remove\tRemoves a person from address book");
            Console.WriteLine("list\tLists out all people in the address book");
            Console.WriteLine("search\tSearches for a person in the address book by first name");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }


        private static void AddPerson()
        {
            Console.Clear();

            Person person = new Person();
            
            Console.Write("Enter First Name: ");
            person.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            person.LastName = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            person.PhoneNumber = Console.ReadLine();

            Console.Write("Enter Address 1: ");
            string[] addresses = new string[2];
            addresses[0] = Console.ReadLine();
            Console.Write("Enter Address 2 (Optional): ");
            addresses[1] = Console.ReadLine();
            person.Addresses = addresses;
      
            People.Add(person);
        }

        private static void RemovePerson()
        {
            List<Person> people = FindPeopleByFirstName();

            Console.Clear();

            if (people.Count == 0)
            {
                Console.WriteLine("That person could not be found. Press any key to continue");
                Console.ReadKey();
                return;
            }
            
            if (people.Count == 1)
            {
                RemovePersonFromList(people.Single());
                return;
            }

            Console.WriteLine("Enter the number of the person you want to remove");
          
            for (int i = 0; i < people.Count; i++)
            {
                 Console.WriteLine(i);
                PrintPerson(people.ElementAt(i));
                
            }
            int removePersonNumber = Convert.ToInt32(Console.ReadLine());
            if (removePersonNumber > people.Count - 1 || removePersonNumber < 0)
            {
                Console.WriteLine("That number is invalid. Press any key to continue.");
                Console.ReadKey();
                return;
            }
            RemovePersonFromList(people.ElementAt(removePersonNumber));
        }

        private static void RemovePersonFromList(Person person)
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to remove this person from your address book? (Y/N)");
            PrintPerson(person);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                People.Remove(person);
                Console.Clear();
                Console.WriteLine("Person removed. Press any key to continue.");
                Console.ReadKey();
            }
        }

        private static void SearchPerson()
        {
            List<Person> people = FindPeopleByFirstName();
            Console.Clear();
            if (people.Count == 0)
            {
                Console.WriteLine("That person could not be found. Press any key to continue");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Here are the current people in your address book matching that search:\n");
            foreach (var person in people)
            {
                PrintPerson(person);
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private static List<Person> FindPeopleByFirstName()
        {
            Console.Clear();
            Console.WriteLine("Enter the first name of the person you would like to find.");
            string firstName = Console.ReadLine();
            return People.Where(x => x.FirstName.ToLower() == firstName.ToLower()).ToList();
        }

        private static void ListPeople()
        {
            Console.Clear();
          
            if (People.Count == 0)
            {
                Console.WriteLine("Your address book is empty. Press any key to continue.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Here are the current people in your address book:\n");
            foreach (var person in People)
            {
                PrintPerson(person);
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private static void PrintPerson(Person person)
        {
          
            Console.WriteLine("First Name: " + person.FirstName);
            Console.WriteLine("Last Name: " + person.LastName);
            Console.WriteLine("Phone Number: " + person.PhoneNumber);
            Console.WriteLine("Address 1: " + person.Addresses[0]);
            Console.WriteLine("Address 2: " + person.Addresses[1]);
            Console.WriteLine("-------------------------------------------");
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Addresses { get; set; }
    }
}
