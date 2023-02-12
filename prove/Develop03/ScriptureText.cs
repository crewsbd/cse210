//Scripture class. 

using System;
using System.Collections;
using System.Text.RegularExpressions;

public class ScriptureText
{
    private string _filePath;
    private List<string> _bookFiles;
    private string _lastReference; //For rubric
    private List<Verse> _lastText;  //For rubric

    public ScriptureText(string path)
    {
        _filePath = path;
        _bookFiles = new List<string>(Directory.GetFiles(_filePath));
        _bookFiles.Sort();

    }
    public void PrintBooks()
    {
        foreach (string book in _bookFiles)
        {
            Console.WriteLine(book);
        }
    }
    public List<Verse> GetTexts(string scripture)
    {   //"book name ch#:v#-v#" OR "book name ch#:v#", case insensitive
        const string referencePattern = "^(?<book>.*) (?<chapter>\\d{1,3}):(?<verse_start>\\d{1,3})-*(?<verse_end>\\d*) *$";
        const string versePattern = "^(?<book>.{3}) (?<chapter>\\d{1,3}):(?<verse>\\d{1,3}) (?<text>.*)";

        this._lastReference = scripture; //rubric
        string bookName = "";
        int chapter = 0;
        int verseStart = 0;
        int verseCount = 1;

        Match queryData = Regex.Match(scripture, referencePattern);
        if (queryData.Success)
        {
            bookName = queryData.Groups["book"].Value;
            bookName = bookName.Substring(0, 1).ToUpper() + bookName.Substring(1).ToLower(); //Enforce correct capitalization. Kind of.
            chapter = int.Parse(queryData.Groups["chapter"].Value);
            verseStart = int.Parse(queryData.Groups["verse_start"].Value);
            if (queryData.Groups["verse_end"].Success && queryData.Groups["verse_end"].Value != "")
            {
                verseCount = int.Parse(queryData.Groups["verse_end"].Value) - int.Parse(queryData.Groups["verse_start"].Value) + 1;
            }
            else
            {
                verseCount = 1;
            }
        }
        else
        {
            return null;
        }

        //We've got the information, use it.

        try
        {
            List<Verse> returnVerses = new List<Verse>();
            int[] verses = Enumerable.Range(verseStart, verseCount).ToArray();
            Boolean continueLoop = true;
            string fileName = $"bom/{bookName.Replace(" ", "-").ToLower()}";
            using (StreamReader bookStream = new StreamReader(fileName))
            {
                do
                {
                    string nextLine = bookStream.ReadLine();
                    if (nextLine != null && nextLine != "")
                    {
                        Match verseData = Regex.Match(nextLine, versePattern);
                        //Console.WriteLine($"Regex result {verseData.Groups["book"].Value} {verseData.Groups["chapter"].Value} {verseData.Groups["verse"].Value}");
                        if (int.Parse(verseData.Groups["chapter"].Value) == chapter && verses.Contains(int.Parse(verseData.Groups["verse"].Value))) //if match found
                        {
                            returnVerses.Add(new Verse(verseData.Groups["text"].Value, bookName, chapter, int.Parse(verseData.Groups["verse"].Value)));
                        }
                    }
                    if (bookStream.ReadLine() == null) //If discard line is null => readLine is null => quit
                    {
                        continueLoop = false;
                    }
                } while (continueLoop);

            }
            if (returnVerses.Count > 0) //Getting 0 lists for some reason. Bandaid.
            {
                this._lastText = returnVerses;
                return returnVerses;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occured while reading file. {e.Message}");
            return null;
        }
    }
}