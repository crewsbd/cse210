using System.Text.RegularExpressions;
public class EternalGoal : Goal
{

    private int _timesCompleted;
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        _timesCompleted = 0;
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
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\",\"timescompleted\":\"{_timesCompleted}\"}}";
    }
    public override void DecodeObject(string objectString)
    {
        string regexString = "\\{type:\"(?<type>[A-Za-z]*)\",name:\"(?<name>[A-Za-z ]*)\",description:\"(?<description>[A-Za-z ,'\"]*)\",points:\"(?<points>[0-9]*)\"\\}";
        Match match = Regex.Match(objectString, regexString);
        _name = match.Groups["name"].Value;
        _description = match.Groups["description"].Value;
        _points = int.Parse(match.Groups["points"].Value);
    }
}