using System.Text.RegularExpressions;
using System.Text.Json;
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }
    public SimpleGoal(JsonElement saveInfo) : base(saveInfo.GetProperty("name").GetString(), saveInfo.GetProperty("description").GetString(), int.Parse(saveInfo.GetProperty("points").GetString()))
    {
        _completed = (saveInfo.GetProperty("completed").GetString() == "False"? false: true);
    }
    public override string Serialize()
    {
        return $"[{(_completed?"✓":" ")}] {_name} ({_description})";
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
        return $"{{\"$type\":\"{GoalType()}\",\"name\":\"{_name}\",\"description\":\"{_description}\",\"points\":\"{_points}\",\"completed\":\"{_completed}\"}}";
    }
}