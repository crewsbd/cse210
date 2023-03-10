/* 
Class to load user data.
*/
using System.Text.Json;

public class SaveData
{
    private string _fileName;
    private string _userName;
    private List<Goal> _saveData;

    public SaveData(string userName, List<Goal> saveData)
    {
        _userName = userName;
        _fileName = $"data_{string.Join('_', userName.ToLower().Split(" "))}.json";
        _saveData = saveData;
    }
    public void Save()
    {
        using (StreamWriter dataFile = new StreamWriter(_fileName))
        {
            dataFile.WriteLine($"{{\n\t\"username\":\"{_userName}\",\n\t\"goals\":[");
            for (int i = 0; i < _saveData.Count; i++)
            {
                dataFile.WriteLine($"\t\t{_saveData[i].EncodeObject()}{(_saveData.Count == i + 1 ? "" : ",")}");
            }
            dataFile.WriteLine("\t]\n}");
        }
    }
    public void Load()
    {
        if (File.Exists(_fileName))
        {
            using (StreamReader dataFile = new StreamReader(_fileName))
            {
                _saveData.Clear();
                string fullText = dataFile.ReadToEnd();
                JsonDocument json = JsonDocument.Parse(fullText);

                JsonElement goals = json.RootElement.GetProperty("goals");
                foreach (JsonElement goal in goals.EnumerateArray())
                {
                    string type = goal.GetProperty("$type").GetString();
                    switch (type)
                    {
                        case "SimpleGoal":
                            {
                                _saveData.Add(new SimpleGoal(goal));
                                break;
                            }
                            case "EternalGoal":
                            {
                                _saveData.Add(new EternalGoal(goal));
                                break;
                            }
                            case "ChecklistGoal":
                            {
                                _saveData.Add(new ChecklistGoal(goal));
                                break;
                            }

                    }
                }
            }
            Console.WriteLine($"{_fileName} loaded.");
        }
        else
        {
            Console.WriteLine("There is no user by that name. Create some goals and then save them to create a file for that user.");
        }
    }
}