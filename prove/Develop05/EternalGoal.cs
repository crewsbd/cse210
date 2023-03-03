public class EternalGoal : Goal
{

    private int _timesCompleted;
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        _timesCompleted = 0;
    }
    public override string Serialize()
    {
        string returnString = $"☒ {_name} ({_description})";
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
}