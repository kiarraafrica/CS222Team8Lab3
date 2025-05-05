using System;
using System.Collections.Generic;
using System.IO;

namespace DigitalDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            bool closeApp = false;

            while (!closeApp)
            {
                IDiary myDiary = null;
                string diaryType = "";

                while (myDiary == null && !closeApp)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Your Digital Diary!");
                    Console.WriteLine("A safe space for your thoughts, memories, and reflections.\n");
                    Console.WriteLine("Choose your Diary Type:");
                    Console.WriteLine("1. Normal Diary");
                    Console.WriteLine("2. Secure Diary");
                    Console.WriteLine("3. Close Application");
                    Console.Write("Enter your choice (1, 2 or 3): ");
                    string? typeChoice = Console.ReadLine();

                    if (typeChoice == "1")
                    {
                        myDiary = new Diary();
                        diaryType = "Normal Diary";
                    }
                    else if (typeChoice == "2")
                    {
                        try
                        {
                            myDiary = new SecureDiary();
                            diaryType = "Secure Diary";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nError: {ex.Message}");
                            Console.WriteLine("Please try again...");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                    }
                    else if (typeChoice == "3")
                    {
                        closeApp = true;
                        Console.WriteLine("\nClosing application...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice. Please enter 1, 2, or 3.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }

                if (closeApp) break;

                bool exitToSelection = false;

                while (!exitToSelection)
                {
                    Console.Clear();
                    Console.WriteLine($"Digital Diary - {diaryType} - Main Menu");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("1. Write a New Entry");
                    Console.WriteLine("2. View All Entries");
                    Console.WriteLine("3. Search Entries by Date");
                    Console.WriteLine("4. Edit an Entry");
                    Console.WriteLine("5. Delete an Entry");
                    Console.WriteLine("6. Exit");
                    Console.Write("Choose an option (1 to 6): ");
                    string? menuInput = Console.ReadLine();

                    switch (menuInput)
                    {
                        case "1":
                            WriteNewEntry(myDiary);
                            break;
                        case "2":
                            ViewAllEntries(myDiary);
                            break;
                        case "3":
                            SearchByDate(myDiary);
                            break;
                        case "4":
                            EditEntry(myDiary);
                            break;
                        case "5":
                            DeleteEntry(myDiary);
                            break;
                        case "6":
                        case "exit":
                            exitToSelection = true;
                            Console.WriteLine("\nReturning to diary selection...");
                            break;
                        default:
                            Console.WriteLine("\nInvalid option. Please select between 1 and 6.");
                            break;
                    }

                    if (!exitToSelection)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
            }

            Console.WriteLine("\nThank you for using Digital Diary. See you next time!");
        }

        static void WriteNewEntry(IDiary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Write New Entry");
                Console.WriteLine("------------------");
                Console.Write("Start typing your entry (type 'x' to cancel):\n\n");
                string? entryText = Console.ReadLine();

                if (entryText?.ToLower() == "x")
                {
                    Console.WriteLine("\nEntry cancelled.");
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(entryText))
                {
                    myDiary.WriteEntry(entryText);
                    return;
                }
                else
                {
                    Console.WriteLine("\nEntry cannot be empty. Try again...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void ViewAllEntries(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("Your Diary Entries");
            Console.WriteLine("----------------------");
            List<string> entries = myDiary.GetAllEntriesAsList();

            if (entries.Count == 0)
            {
                Console.WriteLine("\nNo entries found.");
            }
            else
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {entries[i]}");
                }
            }
        }

        static void SearchByDate(IDiary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Search Entries by Date");
                Console.WriteLine("--------------------------");
                Console.Write("Enter a date (YYYY-MM-DD) or type 'x' to cancel: ");
                string? searchDateInput = Console.ReadLine();

                if (searchDateInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nSearch cancelled.");
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(searchDateInput))
                {
                    List<string> results = myDiary.SearchByDate(searchDateInput.Trim());

                    if (results.Count > 0)
                    {
                        Console.WriteLine("\nEntries on this date:");
                        foreach (string entry in results)
                        {
                            Console.WriteLine(entry);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo entries found for this date.");
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("\nDate input cannot be empty. Try again...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void EditEntry(IDiary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Edit an Entry");
                Console.WriteLine("-----------------");
                List<string> entries = myDiary.GetAllEntriesAsList();

                if (entries.Count == 0)
                {
                    Console.WriteLine("\nNo entries to edit.");
                    return;
                }

                Console.WriteLine("Current Entries:");
                for (int i = 0; i < entries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {entries[i]}");
                }

                Console.Write("\nEnter the entry number to edit (or 'x' to cancel): ");
                string? editNumInput = Console.ReadLine();

                if (editNumInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nEdit cancelled.");
                    return;
                }

                if (int.TryParse(editNumInput, out int editNumber) && editNumber >= 1 && editNumber <= entries.Count)
                {
                    int editIndex = editNumber - 1;
                    Console.WriteLine($"\nCurrent Entry: {entries[editIndex]}");

                    while (true)
                    {
                        Console.Write("Enter the updated text (or 'x' to cancel): ");
                        string? newText = Console.ReadLine();

                        if (newText?.ToLower() == "x")
                        {
                            Console.WriteLine("\nEdit cancelled.");
                            return;
                        }
                        else if (!string.IsNullOrWhiteSpace(newText))
                        {
                            myDiary.EditEntry(editIndex, newText);
                            Console.WriteLine("\nEntry updated successfully!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Entry text cannot be empty. Try again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid number. Please select a valid entry.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        static void DeleteEntry(IDiary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete an Entry");
                Console.WriteLine("-------------------");
                List<string> entries = myDiary.GetAllEntriesAsList();

                if (entries.Count == 0)
                {
                    Console.WriteLine("\nNo entries to delete.");
                    return;
                }

                Console.WriteLine("Current Entries:");
                for (int i = 0; i < entries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {entries[i]}");
                }

                Console.Write("\nEnter the entry number to delete (or 'x' to cancel): ");
                string? deleteNumInput = Console.ReadLine();

                if (deleteNumInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nDelete cancelled.");
                    return;
                }

                if (int.TryParse(deleteNumInput, out int deleteNumber) && deleteNumber >= 1 && deleteNumber <= entries.Count)
                {
                    int deleteIndex = deleteNumber - 1;
                    Console.WriteLine($"\nYou are about to delete: {entries[deleteIndex]}");

                    while (true)
                    {
                        Console.Write("Are you sure? (y/n or x to cancel): ");
                        string? confirmInput = Console.ReadLine();

                        if (confirmInput?.ToLower() == "x" || confirmInput?.ToLower() == "n")
                        {
                            Console.WriteLine("\nDelete cancelled.");
                            return;
                        }
                        else if (confirmInput?.ToLower() == "y")
                        {
                            myDiary.DeleteEntry(deleteIndex);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'y', 'n', or 'x'.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid number. Please choose a valid entry.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }
    }
}
