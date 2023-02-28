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
        return $"{(_completed?"☑":"☒")} {_name}({_description}) -- Currently completed: {_completions}/{_targetCompletions}";;
    }
    public override void MarkComplete()
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
    public override bool Completed()
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
        return _completions * _points + (Completed()?_bonus:0);
    }
}