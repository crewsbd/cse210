using System.Text.RegularExpressions;
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }
    public override string Serialize()
    {
        return $"[{(_completed?"V":" ")}] {_name}({_description})";
    }
    public override bool IsComplete()
    {
        if(_completed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override int GetTotalPoints()
    {
        if(IsComplete())
        {
            return _points;
        }
        else
        {
            return 0;
        }
    }
    public override void RecordEvent()
    {
        if(!_completed)
        {
            _completed = true;
        }
    }
    public override string EncodeObject()
    {
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\"}}";
    }
    public override void DecodeObject(string objectString)
    {
        string regexString = "^\\{\"\\$type\":\"(?<type>[^\"]*)\",\"name\":\"(?<name>[^\"]*)\",\"description\":\"(?<description>[^\"]*)\",\"points\":\"(?<points>[0-9]*)\"\\}";
        


        Match match = Regex.Match(objectString, regexString);
        _name = match.Groups["name"].Value;
        _description = match.Groups["description"].Value;
        _points = int.Parse(match.Groups["points"].Value);
    }

}