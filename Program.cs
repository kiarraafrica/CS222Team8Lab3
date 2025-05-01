using System;
using System.Collections.Generic;
using System.IO;

namespace DigitalDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("   Welcome to Your Digital Diary!   ");
            Console.WriteLine("=====================================");
            Console.WriteLine();

            IDiary myDiary = null;

            // Diary Type Selection Loop
            while (myDiary == null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("        Select Diary Type           ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Normal Diary");
                Console.WriteLine("2. Secure Diary");
                Console.WriteLine("=====================================");
                Console.Write("Enter choice (1 or 2): ");
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
                        Console.WriteLine("Returning to selection...");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter 1 or 2.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }

            bool exit = false;

            // Main Menu Loop
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("          Digital Diary Menu         ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Write New Entry");
                Console.WriteLine("2. View All Entries");
                Console.WriteLine("3. Search by Date (YYYY-MM-DD)");
                Console.WriteLine("4. Edit Entry");
                Console.WriteLine("5. Delete Entry");
                Console.WriteLine("6. Exit");
                Console.WriteLine("=====================================");
                Console.WriteLine("Enter 'x' to cancel action");
                Console.WriteLine("=====================================");
                Console.Write("Select an option: ");
                string? menuInput = Console.ReadLine();

                if (menuInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nReturning to menu...");
                    Console.WriteLine("Press any key to continue...");
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
                        Console.WriteLine("\nExiting Digital Diary. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option selected. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Method for Writing New Entry
        static void WriteNewEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("        Write New Entry              ");
            Console.WriteLine("=====================================");
            Console.WriteLine("Enter your diary entry (or 'x' to cancel):");
            string? entryText = Console.ReadLine();

            if (entryText?.ToLower() == "x")
            {
                Console.WriteLine("\nWrite cancelled.");
            }
            else if (!string.IsNullOrWhiteSpace(entryText))
            {
                myDiary.WriteEntry(entryText);
                Console.WriteLine("\nEntry saved!");
            }
            else
            {
                Console.WriteLine("\nEntry cannot be empty.");
            }
        }

        // Method for Viewing All Entries
        static void ViewAllEntries(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("        View All Entries            ");
            Console.WriteLine("=====================================");
            List<string> entriesToView = myDiary.GetAllEntriesAsList();

            if (entriesToView.Count == 0)
            {
                Console.WriteLine("\nNo entries to display.");
            }
            else
            {
                for (int i = 0; i < entriesToView.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {entriesToView[i]}");
                }
            }
            Console.WriteLine("\n=====================================");
        }

        // Method for Searching Entries by Date
        static void SearchByDate(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("       Search Entries by Date       ");
            Console.WriteLine("=====================================");
            Console.Write("Enter date (YYYY-MM-DD) (or 'x' to cancel): ");
            string? searchDateInput = Console.ReadLine();

            if (searchDateInput?.ToLower() == "x")
            {
                Console.WriteLine("\nSearch cancelled.");
            }
            else if (!string.IsNullOrWhiteSpace(searchDateInput))
            {
                List<string> results = myDiary.SearchByDate(searchDateInput.Trim());

                if (results.Count > 0)
                {
                    Console.WriteLine("\n--- Search Results ---");
                    foreach (string foundEntry in results)
                    {
                        Console.WriteLine(foundEntry);
                    }
                    Console.WriteLine("\n--- End of Results ---");
                }
                else
                {
                    Console.WriteLine("\nNo entries found for the specified date.");
                }
            }
            else
            {
                Console.WriteLine("\nSearch date cannot be empty.");
            }
        }

        // Method for Editing an Entry
        static void EditEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("           Edit Entry               ");
            Console.WriteLine("=====================================");
            List<string> entriesToEdit = myDiary.GetAllEntriesAsList();

            if (entriesToEdit.Count == 0)
            {
                Console.WriteLine("\nNo entries to edit.");
                return;
            }

            Console.WriteLine("Entries available to edit:");
            for (int i = 0; i < entriesToEdit.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entriesToEdit[i]}");
            }

            Console.Write("Enter number of entry to edit (or 'x' to cancel): ");
            string? editNumInput = Console.ReadLine();

            if (editNumInput?.ToLower() == "x")
            {
                Console.WriteLine("\nEdit cancelled.");
                return;
            }

            int editNumber;
            if (int.TryParse(editNumInput, out editNumber) && editNumber >= 1 && editNumber <= entriesToEdit.Count)
            {
                int editIndex = editNumber - 1;
                Console.WriteLine($"\nCurrent text: {entriesToEdit[editIndex]}");
                Console.Write("Enter new text (or 'x' to cancel): ");
                string? newText = Console.ReadLine();

                if (newText?.ToLower() == "x")
                {
                    Console.WriteLine("\nEdit cancelled.");
                }
                else
                {
                    myDiary.EditEntry(editIndex, newText ?? "");
                    Console.WriteLine("\nEntry updated.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid entry number.");
            }
        }

        // Method for Deleting an Entry
        static void DeleteEntry(IDiary myDiary)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("         Delete Entry               ");
            Console.WriteLine("=====================================");
            List<string> entriesToDelete = myDiary.GetAllEntriesAsList();

            if (entriesToDelete.Count == 0)
            {
                Console.WriteLine("\nNo entries to delete.");
                return;
            }

            Console.WriteLine("Entries available to delete:");
            for (int i = 0; i < entriesToDelete.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entriesToDelete[i]}");
            }

            Console.Write("Enter number of entry to delete (or 'x' to cancel): ");
            string? deleteNumInput = Console.ReadLine();

            if (deleteNumInput?.ToLower() == "x")
            {
                Console.WriteLine("\nDelete cancelled.");
                return;
            }

            int deleteNumber;
            if (int.TryParse(deleteNumInput, out deleteNumber) && deleteNumber >= 1 && deleteNumber <= entriesToDelete.Count)
            {
                int deleteIndex = deleteNumber - 1;
                Console.WriteLine($"\nYou selected to delete: {entriesToDelete[deleteIndex]}");
                Console.Write("Confirm deletion? (y/n) (or 'x' to cancel): ");
                string? confirmInput = Console.ReadLine();

                if (confirmInput?.ToLower() == "x")
                {
                    Console.WriteLine("\nDelete cancelled.");
                }
                else if (confirmInput?.ToLower() == "y")
                {
                    myDiary.DeleteEntry(deleteIndex);
                    Console.WriteLine("\nEntry deleted.");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid entry number.");
            }
        }
    }
}
