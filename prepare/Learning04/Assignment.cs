public class Assignment
{
    private string _studentName;
    private string _topic;

    public Assignment(string name, string topic)
    {
        _studentName = name;
        _topic = topic;
    }
    public string GetSummary()
    {
        return $"Name: {_studentName}, Topic: {_topic}";
    }
    public string StudentName
    {
        get => _studentName;
    }
    public string Topic
    {
        get => _topic;
    }

}