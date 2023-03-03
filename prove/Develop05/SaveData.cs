/* Todo: Decide on data structure
-JSON? Save some games stats, then save array of goals. 
-Need to track data for different goal types. 
-Need conditionals on save and load? 
-Need regex?
-Is there any information that can't be obtained from _saveData? A name? Make a separate struct for this?

*/

public class SaveData
{
    private string _fileName;
    private List<Goal> _saveData;

    public SaveData(string fileName, List<Goal> saveData)
    {
        if(File.Exists(_fileName))
        {
            _fileName = fileName;
        }
        else
        {
            throw new FileNotFoundException($"{_fileName} does not exist.");
        }

        _saveData = saveData;
    }
    public void Save()
    {
        StreamWriter dataFile = new StreamWriter(_fileName);
        dataFile.WriteLine("\"EternalQuestSaveData\": {{");
        dataFile.WriteLine($"    \"GameStats\": {{ }}");



        dataFile.WriteLine("}}");
    }
    public void Load()
    {
        StreamReader dataFile = new StreamReader(_fileName);
    }
}