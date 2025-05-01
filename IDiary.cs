using System.Collections.Generic;

public interface IDiary
{
    void WriteEntry(string text);
    // Adds a new entry to the diary.
    // param text = content w/ date

    List<string> GetAllEntriesAsList();
    // Retrieves all entries currently stored in the diary
    // List of strings, where each string is a complete diary entry

    List<string> SearchByDate(string date);
    // Searches for diary entries that match a specific date
    // param date = The date to search for (format: YYYY-MM-DD).

    bool EditEntry(int index, string newText);
    // Edits an existing entry at the specified index
    // param index = loaction zero-based index
    // param newText = new content
    // returns True if the deletion was successful, false otherwise (like invalid index or newText)

    bool DeleteEntry(int index);
    // Deletes an existing entry at the specified index
    // param index = loaction zero-based index/
    // returns True if the deletion was successful, false otherwise (like invalid index)
}