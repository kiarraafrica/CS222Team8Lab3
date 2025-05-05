using System;
using System.Collections.Generic;
using System.IO;

namespace DigitalDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Your Digital Diary!\n");

            IDiary myDiary = null;

            // Diary Type Selection
            while (myDiary == null)
            {
                Console.WriteLine("Select Diary Type:");
                Console.WriteLine("1. Normal Diary");
                Console.WriteLine("2. Secure Diary");
                Console.Write("Choice (1 or 2): ");
                string? typeChoice = Console.ReadLine();

                if (typeChoice == "1")
                {
                    myDiary = new Diary();
                    Console.WriteLine("\nNormal Diary selected.");
                }
                else if (typeChoice == "2")
                {
                    try
                    {
                        myDiary = new SecureDiary();
                        Console.WriteLine("\nSecure Diary selected.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Please try again...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Enter 1 or 2.");
                    Console.ReadKey();
                }
            }

            bool exit = false;

            // Main Menu Loop
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Digital Diary Menu");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Write Entry");
                Console.WriteLine("2. View Entries");
                Console.WriteLine("3. Search by Date");
                Console.WriteLine("4. Edit Entry");
                Console.WriteLine("5. Delete Entry");
                Console.WriteLine("6. Exit");
                Console.Write("Option (1-6 or x to cancel): ");
                string? menuInput = Console.ReadLine();

                if (menuInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nCancelled.");
                    Console.ReadKey();
                    continue;
                }

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
                        exit = true;
                        Console.WriteLine("\nGoodbye!");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                }
            }
        }

        // Write New Entry
        static void WriteNewEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("Write New Entry");
            Console.WriteLine("---------------");
            Console.Write("Entry (or x to cancel):\n");
            Console.WriteLine();
            string? entryText = Console.ReadLine();

            if (entryText?.ToLower() == "x")
            {
                Console.WriteLine("\nCancelled.");
            }
            else if (!string.IsNullOrWhiteSpace(entryText))
            {
                myDiary.WriteEntry(entryText);
                Console.WriteLine("Entry saved.");
            }
            else
            {
                Console.WriteLine("\nEntry cannot be empty.");
            }
        }

        // View All Entries
        static void ViewAllEntries(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("All Entries");
            Console.WriteLine("-----------");
            List<string> entries = myDiary.GetAllEntriesAsList();

            if (entries.Count == 0)
            {
                Console.WriteLine("\nNo entries.");
            }
            else
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {entries[i]}");
                }
            }
        }

        // Search Entries by Date
        static void SearchByDate(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("Search by Date");
            Console.WriteLine("--------------");
            Console.Write("Date (YYYY-MM-DD or x to cancel): ");
            string? searchDateInput = Console.ReadLine();

            if (searchDateInput?.ToLower() == "x")
            {
                Console.WriteLine("\nCancelled.");
            }
            else if (!string.IsNullOrWhiteSpace(searchDateInput))
            {
                List<string> results = myDiary.SearchByDate(searchDateInput.Trim());

                if (results.Count > 0)
                {
                    Console.WriteLine("\nResults:");
                    foreach (string entry in results)
                    {
                        Console.WriteLine(entry);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo entries found.");
                }
            }
            else
            {
                Console.WriteLine("\nDate cannot be empty.");
            }
        }

        // Edit an Entry
        static void EditEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("Edit Entry");
            Console.WriteLine("----------");
            List<string> entries = myDiary.GetAllEntriesAsList();

            if (entries.Count == 0)
            {
                Console.WriteLine("\nNo entries.");
                return;
            }

            Console.WriteLine("Entries:");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }

            Console.Write("\nEntry number (or x to cancel): ");
            string? editNumInput = Console.ReadLine();

            if (editNumInput?.ToLower() == "x")
            {
                Console.WriteLine("\nCancelled.");
                return;
            }

            int editNumber;
            if (int.TryParse(editNumInput, out editNumber) && editNumber >= 1 && editNumber <= entries.Count)
            {
                int editIndex = editNumber - 1;
                Console.WriteLine($"\nCurrent: {entries[editIndex]}");
                Console.Write("New text (or x to cancel): ");
                string? newText = Console.ReadLine();

                if (newText?.ToLower() == "x")
                {
                    Console.WriteLine("\nCancelled.");
                }
                else
                {
                    myDiary.EditEntry(editIndex, newText ?? "");
                    Console.WriteLine("\nEntry updated.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid number.");
            }
        }

        // Delete an Entry
        static void DeleteEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("Delete Entry");
            Console.WriteLine("------------");
            List<string> entries = myDiary.GetAllEntriesAsList();

            if (entries.Count == 0)
            {
                Console.WriteLine("\nNo entries.");
                return;
            }

            Console.WriteLine("Entries:");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }

            Console.Write("\nEntry number (or x to cancel): ");
            string? deleteNumInput = Console.ReadLine();

            if (deleteNumInput?.ToLower() == "x")
            {
                Console.WriteLine("\nCancelled.");
                return;
            }

            int deleteNumber;
            if (int.TryParse(deleteNumInput, out deleteNumber) && deleteNumber >= 1 && deleteNumber <= entries.Count)
            {
                int deleteIndex = deleteNumber - 1;
                Console.WriteLine($"\nDelete: {entries[deleteIndex]}");
                Console.Write("Confirm? (y/n or x to cancel): ");
                string? confirmInput = Console.ReadLine();

                if (confirmInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nCancelled.");
                }
                else if (confirmInput?.ToLower() == "y")
                {
                    myDiary.DeleteEntry(deleteIndex);
                    Console.WriteLine("\nEntry deleted.");
                }
                else
                {
                    Console.WriteLine("\nCancelled.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid number.");
            }
        }
    }
}