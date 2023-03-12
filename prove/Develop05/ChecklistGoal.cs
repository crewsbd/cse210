using System.Text.RegularExpressions;
using System.Text.Json;

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
    public ChecklistGoal(JsonElement saveInfo) : base(saveInfo.GetProperty("name").GetString(), saveInfo.GetProperty("description").GetString(), int.Parse(saveInfo.GetProperty("points").GetString()))
    {
        _bonus = int.Parse(saveInfo.GetProperty("bonus").GetString());
        _completions = int.Parse(saveInfo.GetProperty("completions").GetString());
        _targetCompletions = int.Parse(saveInfo.GetProperty("targetcompletions").GetString());
        _completed = (saveInfo.GetProperty("completed").GetString() == "False" ? false : true);
    }
    public override string Serialize()
    {
        return $"[{(_completed ? "✓" : " ")}] {_name} ({_description}) -- Currently completed: {_completions}/{_targetCompletions}"; ;
    }
    public override void RecordEvent()
    {
        if (_completions < _targetCompletions)
        {
            _completions++;
            if (_completions == _targetCompletions)
            {
                _completed = true;
            }
        }
    }
    public override bool IsComplete()
    {
        if (_completions >= _targetCompletions)
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
        return _completions * _points + (IsComplete() ? _bonus : 0);
    }
    public override string EncodeObject()
    {
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\",\"completed\":\"{_completed}\",\"bonus\":\"{_bonus}\",\"completions\":\"{_completions}\",\"targetcompletions\":\"{_targetCompletions}\"}}";
    }
}