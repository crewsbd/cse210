public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }
    public override string Serialize()
    {
        return $"{(_completed?"☑":"☒")} {_name}({_description})";
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

}