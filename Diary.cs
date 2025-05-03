using System;
using System.IO;
using System.Collections.Generic;

public class Diary : IDiary
{
    protected readonly string filePath;

    // Constructor
    public Diary(string diaryFilePath = "diary.txt")
    {
        this.filePath = Path.GetFullPath(diaryFilePath);

        try
        {
            string? directoryPath = Path.GetDirectoryName(this.filePath);

            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper Method to read all lines in the txt file
    protected virtual List<string> ReadAllLinesToList()
    {
        List<string> entries = new List<string>();
        if (!File.Exists(this.filePath))
        {
            return entries;
        }

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    entries.Add(line);
                }
            }
        }

        catch (IOException ex)
        {
            Console.WriteLine($"Error reading diary file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while reading file: {ex.Message}");
        }

        return entries;
    }

    // Helper method to be use in updating an entry
    protected virtual bool WriteLinesToFile(List<string> lines)
    {
        try
        {
            File.WriteAllLines(filePath, lines);
            return true;
        }

        catch (IOException ex)
        {
            Console.WriteLine($"Error writing diary file: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while writing file: {ex.Message}");
            return false;
        }
    }

    // Implementation of WriteEntry method
    public virtual void WriteEntry(string text)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine($"{timestamp} | {text}");
            }
            Console.WriteLine("Entry added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding entry: {ex.Message}");
        }
    }

    // Implementation of getAllEntriesAsList method
    public virtual List<string> GetAllEntriesAsList()
    {
        return ReadAllLinesToList();
    }

    // Implementation of SearchByDate method
    public virtual List<string> SearchByDate(string date)
    {
        List<string> allEntries = ReadAllLinesToList();
        List<string> foundEntries = new List<string>();

        foreach (string entry in allEntries)
        {
            if (entry.Trim().StartsWith(date))
            {
                foundEntries.Add(entry);
            }
        }

        if (foundEntries.Count == 0)
        {
            Console.WriteLine($"No entries found starting with date: {date}");
        }

        return foundEntries;
    }

    public virtual bool EditEntry(int index, string newText)
    {
        // implementation here - Kiarra
        // use ReadAllLinesToList (Helper method) - To get all enties in the file
        // use WriteLinesToFile (Helper Method) - Write the updated list back to the file
        List<string> entries = ReadAllLinesToList();

        if (index < 0 || index >= entries.Count)
        {
            Console.WriteLine("Entry does not exist.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(newText))
        {
            Console.WriteLine("New text cannot be empty.");
            return false;
        }

        try
        {
            // get the existing entry from the list
            string existingEntry = entries[index];
            int separatorIndex = existingEntry.IndexOf(" | ");
            if (separatorIndex != -1)
            {
                string timestamp = existingEntry.Substring(0, separatorIndex + 3);
                entries[index] = timestamp + newText;
            }
            else
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | ";
                entries[index] = timestamp + newText;
            }

            // save the changes back to file
            return WriteLinesToFile(entries);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error editing entry: {ex.Message}");
            return false;
        }
      
    }
 
    public virtual bool DeleteEntry(int index)
    {
        // implementation here - Kiel
        // use ReadAllLinesToList (Helper method)
    }
}