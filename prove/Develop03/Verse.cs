
public class Verse
{
    private string _book;
    private int _chapterNumber;
    private int _verseNumber;
    private string _template;
    private List<Word> _words;
    private Random _random;

    
    public Verse(string text, string book, int chapterNum, int verseNum)
    {
        _book = book;
        _chapterNumber = chapterNum;
        _verseNumber = verseNum;
        _template = text;
        _words = new List<Word>();
        _random = new Random(DateTime.Now.Millisecond);
        string[] splitWords = text.Split(' ');
        foreach(string word in splitWords)
        {
            _words.Add(new Word(word.Trim()));
        }
    }

    public string Book
    {
        get => _book;
    }
    public int Chapter
    {
        get => _chapterNumber;
    }

    public Boolean AllHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.Hidden())
            {
                return false;
            }
        }
        return true;
    }
    public void HidePercent(int percent)
    {

    }
    public void HideNumber(int number)
    {
        for(int i = 0; i < number; i++) 
        {
            int wordToHide = _random.Next(_words.Count());
            if(!_words[wordToHide].Hidden())
            {
                _words[wordToHide].Hide();
            }
            else if(! this.AllHidden())
            {
                i--;
            }
        }
    }
    public string Serialize()
    {
        string newString = $"{_verseNumber.ToString()}.  ";
        for(int word = 0; word < _words.Count(); word++)
        {
            newString += _words[word].GetWord() + " ";
        }
        return newString;
    }
}