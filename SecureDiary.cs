using System;
using System.Collections.Generic;
using System.IO;

public class SecureDiary : Diary
{
    private const string PasswordTag = "Your password: ";

    public SecureDiary(string secureFilePath = "secure_diary.txt") : base(secureFilePath)
    {
        // TODO: Initialize the secure diary
        // 1. Display message showing Secure Diary
        // 2. Check if file exists:
        //    - If exists, prompt for password and verify
        //    - If not, prompt for new password and create file
        // 3. Pause with "Press any key to continue..."
    }

    private string ReadPasswordFromFile()
    {
        // TODO: Read the password from the first line of the file
        // 1. Check if file exists
        // 2. Read first line and extract password after PasswordTag
        // 3. Handle errors (file not found, invalid password line)
        throw new NotImplementedException(); // remove if done implementing. Thanks:)
    }

    protected override List<string> ReadAllLinesToList()
    {
        // TODO: Read diary entries, skipping the password line
        // 1. Return empty list if file doesn't exist
        // 2. Skip first line (password)
        // 3. Read remaining lines into a list
        throw new NotImplementedException(); // remove if done implementing. Thanks:)
    }

    protected override bool WriteLinesToFile(List<string> entries)
    {
        // TODO: Write password and entries to file
        // 1. Read current password
        // 2. Create list with password line followed by entries
        // 3. Write to file
        // 4. Handle errors
        throw new NotImplementedException(); // remove if done implementing. Thanks:)
    }
}
