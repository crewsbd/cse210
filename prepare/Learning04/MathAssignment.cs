public class MathAssignment : Assignment
{
    private string _textbookSection;
    private string _problems;

    public MathAssignment(string name, string topic, string section, string problems) : base(name, topic)
    {
        _textbookSection = section;
        _problems = problems;
    }
    public string GetHomeworkList()
    {
        return $"Name: {StudentName}, Topic: {Topic}, Section: {_textbookSection}, Problems {_problems}";
    }
}