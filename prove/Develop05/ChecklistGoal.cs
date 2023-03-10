using System.Text.RegularExpressions;
public class ChecklistGoal : Goal
{
    private int _bonus;
    private int _completions;
    private int _targetCompletions;


    public ChecklistGoal(string name, string description, int points, int targetCompletions, int bonus) : base(name, description, points)
    {
        _bonus = bonus;
        _targetCompletions = targetCompletions;
    }
    public override string Serialize()
    {
        return $"[{(_completed?"V":" ")}] {_name}({_description}) -- Currently completed: {_completions}/{_targetCompletions}";;
    }
    public override void RecordEvent()
    {
        if(_completions < _targetCompletions)
        {
            _completions++;
            if(_completions == _targetCompletions)
            {
                _completed = true;
            }
        }
    }
    public override bool IsComplete()
    {
        if(_completions >= _targetCompletions)
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
        return _completions * _points + (IsComplete()?_bonus:0);
    }
    public override string EncodeObject()
    {
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\",\"bonus\":\"{_bonus}\",\"completions\":\"{_completions}\",\"targetcompletions\":\"{_targetCompletions}\"}}";
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