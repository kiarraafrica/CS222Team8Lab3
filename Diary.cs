using System;
using System.IO;
using System.Collections.Generic;

public class Diary
{
    private readonly string filePath;

    public Diary(string path)
    {
        filePath = path;
    }

    public void writeEntry(string text)
    {
        try
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {text}";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(entry);
            }
            Console.WriteLine("Entry added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding entry: {ex.Message}");
        }
    }

    public void viewAllEntries()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No entries found. The diary is empty.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading entries: {ex.Message}");
        }
    }

    public void searchByDate(string date)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No entries found. The diary is empty.");\
                return;
            }

            bool found = false;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(date))
                    {
                        Console.WriteLine(line);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"No entries found for date: {date}");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error searching the entries: {ex.Message}");
        }

    }

}