using System;

class Program
{
    static void Main(string[] args)
    {
        Diary diary = new Diary("diary.txt");
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Digital Diary ===");
            Console.WriteLine("1. Write a New Entry");
            Console.WriteLine("2. View All Entries");
            Console.WriteLine("3. Search Entry by Date");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter your diary entry: ");
                    string entry = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(entry))
                    {
                        diary.writeEntry(entry);
                    }
                    else
                    {
                        Console.WriteLine("Entry cannot be empty!");
                    }
                    break;

                case "2":
                    Console.WriteLine("\nAll Diary Entries:");
                    diary.viewAllEntries();
                    break;

                case "3":
                    Console.Write("Enter the date to search (YYYY-MM-DD): ");
                    string date = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(date))
                    {
                        Console.WriteLine($"\nEntries for {date}:");
                        diary.searchByDate(date);
                    }
                    else
                    {
                        Console.WriteLine("Date cannot be empty!");
                    }
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose between 1 and 4.");
                    break;
            }
        }
    }
}