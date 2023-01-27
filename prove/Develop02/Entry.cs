using System;

class Entry 
{
    private string _response;
    private string _prompt;
    private string _entryDate;

    public Entry(string prompt, string response, string date)
    {
        _prompt = prompt;
        _response = response;
        _entryDate = date;
    }
    public string FormatedEntry()
    {
        return Boxit.ShadowBox($"{_entryDate} - {_prompt}\n\n{_response}", width:40);
    }
    public void ChangeEntry(int entryIndex)
    {
        return;
    }
    public string GetCSVLine()
    {   //CSV serialize
        return $"\"{_prompt}\",\"{_response}\",\"{_entryDate}\"";
    }
}