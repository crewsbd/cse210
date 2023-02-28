public abstract class Goal
{
    private string _name;
    private Boolean _completed;

    public Goal(string newName)
    {   
        _name = newName;
        _completed = false;
    }
    public string Serialize()
    {
        return "";
    }

}