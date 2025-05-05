class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Your Digital Diary!");
        Console.WriteLine("A safe space for your thoughts, memories, and reflections.\n");

        // Diary Type Selection
        while (true)
        {
            IDiary myDiary = null;

            while (myDiary == null)
            {
                Console.Clear();
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

            bool exitToMain = false;

            // Main Menu Loop
            while (!exitToMain)
            {
                Console.Clear();
                Console.WriteLine("Digital Diary Menu");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Write Entry");
                Console.WriteLine("2. View Entries");
                Console.WriteLine("3. Search by Date");
                Console.WriteLine("4. Edit Entry");
                Console.WriteLine("5. Delete Entry");
                Console.WriteLine("6. Exit to Diary Type Selection");
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
                        exitToMain = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }

                if (!exitToMain)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

    }

    // Write New Entry
    static void WriteNewEntry(IDiary myDiary)
    {
        Console.Clear();
        Console.WriteLine("Write New Entry");
        Console.WriteLine("---------------");
        Console.Write("Start typing your entry (type 'x' to cancel):\n\n");
        Console.WriteLine();
        string? entryText = Console.ReadLine();

        if (entryText?.ToLower() == "x")
        {
            Console.WriteLine("\nEntry cancelled.");
        }
        else if (!string.IsNullOrWhiteSpace(entryText))
        {
            myDiary.WriteEntry(entryText);
            Console.WriteLine("Entry saved successfully!");
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

    // Search Entries by Date
    static void SearchByDate(IDiary myDiary)
    {
        Console.Clear();
        Console.WriteLine("Search Entries by Date");
        Console.WriteLine("--------------------------");
        Console.Write("Enter a date (YYYY-MM-DD) or type 'x' to cancel: ");
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
                Console.WriteLine("\nEntries on this date:");
                foreach (string entry in results)
                {
                    Console.WriteLine(entry);
                }
            }
            else
            {
                Console.WriteLine("\nNo entries found for that date.");
            }
        }
        else
        {
            Console.WriteLine("\nDate input cannot be empty.");
        }
    }

    // Edit an Entry
    static void EditEntry(IDiary myDiary)
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

        int editNumber;
        if (int.TryParse(editNumInput, out editNumber) && editNumber >= 1 && editNumber <= entries.Count)
        {
            int editIndex = editNumber - 1;
            Console.WriteLine($"\nCurrent Entry: {entries[editIndex]}");
            Console.Write("Enter the updated text (or 'x' to cancel): ");
            string? newText = Console.ReadLine();

            if (newText?.ToLower() == "x")
            {
                Console.WriteLine("\nEdit cancelled.");
            }
            else
            {
                myDiary.EditEntry(editIndex, newText ?? "");
                Console.WriteLine("\nEntry updated successfully!");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid number. Please select a valid entry.");
        }
    }

    // Delete an Entry
    static void DeleteEntry(IDiary myDiary)
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

        int deleteNumber;
        if (int.TryParse(deleteNumInput, out deleteNumber) && deleteNumber >= 1 && deleteNumber <= entries.Count)
        {
            int deleteIndex = deleteNumber - 1;
            Console.WriteLine($"\nYou are about to delete: {entries[deleteIndex]}");
            Console.Write("Are you sure? (y/n or x to cancel): ");
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
                Console.WriteLine("\nDelete cancelled.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid number. Please choose a valid entry.");
        }
    }
}