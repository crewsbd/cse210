using System.Text.RegularExpressions;
using System.Text.Json;

public class EternalGoal : Goal
{
    private int _timesCompleted;
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        _timesCompleted = 0;
    }
    public EternalGoal(JsonElement saveInfo) : base(saveInfo.GetProperty("name").GetString(), saveInfo.GetProperty("description").GetString(), int.Parse(saveInfo.GetProperty("points").GetString()))
    {
        _timesCompleted = int.Parse(saveInfo.GetProperty("timescompleted").GetString());
        _completed = (saveInfo.GetProperty("completed").GetString() == "False" ? false : true);
    }
    public override string Serialize()
    {
        string returnString = $"[-] {_name} ({_description}) {_timesCompleted} completions.";
        return $"{returnString}";
    }
    public override int GetTotalPoints()
    {
        return _points * _timesCompleted;
    }
    public override bool IsComplete()
    {
        return false;
    }
    public override void RecordEvent()
    {
        _timesCompleted++;
    }
    public override string EncodeObject()
    {
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\",\"completed\":\"{_completed}\",\"timescompleted\":\"{_timesCompleted}\"}}";
    }
}