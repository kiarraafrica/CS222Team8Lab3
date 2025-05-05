using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SecureDiary : Diary
{
    private const string PasswordTag = "Your password: ";
    private string _password;

    public SecureDiary(string secureFilePath = "secure_diary.txt") : base(secureFilePath)
    {
        // TODO: Initialize the secure diary
        // 1. Display message showing Secure Diary
        // 2. Check if file exists:
        //    - If exists, prompt for password and verify
        //    - If not, prompt for new password and create file
        // 3. Pause with "Press any key to continue...

        Console.WriteLine("\n--- Welcome to Secure Diary! ---");

        if (File.Exists(filePath))
        {
            Console.Write("Enter your password to access the diary: ");
            string enteredPassword = Console.ReadLine();
            _password = ReadPasswordFromFile();

            if (_password != enteredPassword)
            {
                throw new UnauthorizedAccessException("Incorrect password. Access denied.");
            }

            Console.WriteLine("\nAccess granted. Welcome back!");
        }
        else
        {
            Console.Write("Set a new password to create your diary: ");
            _password = Console.ReadLine();
            WriteLinesToFile(new List<string>());
            Console.WriteLine("\nSecure diary created successfully!");
        }

        //Console.WriteLine("\nPress any key to continue...");
        //Console.ReadKey();
    }

    private string ReadPasswordFromFile()
    {
        // TODO: Read the password from the first line of the file
        // 1. Check if file exists
        // 2. Read first line and extract password after PasswordTag
        // 3. Handle errors (file not found, invalid password line)

        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Diary file not found.");

            string firstLine = File.ReadLines(filePath).FirstOrDefault();

            if (firstLine != null && firstLine.StartsWith(PasswordTag))
            {
                return firstLine.Substring(PasswordTag.Length);
            }

            throw new InvalidDataException("Password line is missing or corrupted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError reading password: {ex.Message}");
            Environment.Exit(1);
            return null!;
        }
    }

    protected override List<string> ReadAllLinesToList()
    {
        // TODO: Read diary entries, skipping the password line
        // 1. Return empty list if file doesn't exist
        // 2. Skip first line (password)
        // 3. Read remaining lines into a list

        List<string> entries = new List<string>();

        try
        {
            if (!File.Exists(filePath))
                return entries;

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length > 1)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    entries.Add(lines[i]);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError reading diary entries: {ex.Message}");
        }

        return entries;
    }

    protected override bool WriteLinesToFile(List<string> entries)
    {
        // TODO: Write password and entries to file
        // 1. Read current password
        // 2. Create list with password line followed by entries
        // 3. Write to file
        // 4. Handle errors
        try
        {
            List<string> allLines = new List<string>();
            allLines.Add(PasswordTag + _password);
            allLines.AddRange(entries);
            File.WriteAllLines(filePath, allLines);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError writing to diary file: {ex.Message}");
            return false;
        }
    }
}