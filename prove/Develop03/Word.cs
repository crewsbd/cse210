public class Word
{
    private string _word;
    private Boolean _hidden;

    public Word(string word)
    {
        _word = word;
    }
    public string GetWord()
    {
        if (_hidden)
        {
            return "".PadLeft(_word.Length, '_');
        }
        else
        {
            return _word;
        }
    }
    public void Hide()
    {
        _hidden = true;
    }
    public void Show()
    {
        _hidden = false;
    }
    public Boolean Hidden()
    {
        return _hidden;
    }
}