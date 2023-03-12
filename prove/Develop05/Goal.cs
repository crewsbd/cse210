using System.Text.RegularExpressions;
public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected Boolean _completed;
    protected int _points;

    public Goal(string name, string description, int points)
    {   
        _name = name;
        _description = description;
        _points = points;
        _completed = false;
    }
    public abstract string Serialize();
    public abstract Boolean IsComplete();
    public abstract int GetTotalPoints();
    public abstract void RecordEvent();
    public abstract string EncodeObject();
    public string GoalType()
    {
        return this.GetType().ToString();
    }

}