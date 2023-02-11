using System;

class Journal
{
    private List<Entry> _entries;
    private Prompts _prompts;
    private string _name; //Not really used at this point

    public Journal(string name)
    {
        _entries = new List<Entry>();
        List<string> promptList = File.ReadAllLines("prompts.txt").ToList();
        _prompts = new Prompts(promptList);
        _name = name;
        return;
    }
    public void WriteEntry()
    {
        string prompt = _prompts.GetPrompt(); 
        Console.WriteLine($"{prompt}");
        string response = Console.ReadLine();
        _entries.Add(new Entry(prompt, response, DateTime.Now.ToShortDateString()));
        return;
    }
    public void DisplayEntry(int index)
    {   //Display a single entry
        Console.WriteLine(_entries[index].FormatedEntry());
    }
    public void DisplayEntry()
    {   //Displays all entries
        int linesPerPage = 20;
        int linesOnPage = 0;
        foreach(Entry entry in _entries)
        {
            //Print a set of entries.
            string formattedEntry = entry.FormatedEntry();
            linesOnPage += formattedEntry.Split("\n").Count();
            if(linesOnPage > linesPerPage)
            {
                Console.Write("--MORE--");
                Console.ReadKey();
                linesOnPage = 0;
                Console.WriteLine(formattedEntry);
            }
            else
            {
                Console.WriteLine(formattedEntry);
            }
        }
    }
    public void Load()
    {
        Console.Write("File name: ");
        string fileName = Console.ReadLine();
        if(File.Exists(fileName))
        {
            string[] journalLines = File.ReadAllLines(fileName);
            _entries.Clear();
            foreach (string line in journalLines )
            {
                string[] record = line.Split("\",\"");
                _entries.Add(new Entry(record[0].Replace("\"",""), record[1], record[2].Replace("\"","")));
            }
        }
        else
        {
            Console.WriteLine($"{fileName} does not exist.");
        }
    }
    public void Save()
    {
        Console.Write("File name: ");
        string fileName = Console.ReadLine();
        if(fileName.Length > 0)
        {
            string saveString = "";
            foreach(Entry entry in _entries)
            {
                saveString += entry.GetCSVLine() + "\n";
            }
            File.WriteAllText(fileName, saveString);
        }
        else
        {
            Console.WriteLine("Invalid filename.");
        }
        return;
    }
    public int Count()
    {
        return _entries.Count();
    }
}