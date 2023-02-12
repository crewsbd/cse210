public class Reference
{
    private string _book;
    private int _chapterNumber;
    private List<Verse> _verses;

    public Reference(string[] texts, string book, int chapterNum, int startingVerse)
    {
        _book = book;
        _chapterNumber = chapterNum;
        _verses = new List<Verse>();
        for (int verse = 0; verse < texts.Length; verse++)
        {
            _verses.Add(new Verse(texts[verse], _book, _chapterNumber, startingVerse + verse));
        }
    }
    public Reference(List<Verse> verses)
    {
        _book = verses[0].Book; //They're all the same
        _chapterNumber = verses[0].Chapter; //Same
        _verses = new List<Verse>();
        for (int verseIndex = 0; verseIndex < verses.Count; verseIndex++)
        {
            _verses.Add(verses[verseIndex]);
        }
    }

    public Boolean AllHidden()
    {
        foreach (Verse verse in _verses)
        {
            if (!verse.AllHidden())
            {
                return false;
            }
        }
        return true;
    }
    public void HidePercent(int percent)
    {
        foreach (Verse verse in _verses)
        {
            verse.HidePercent(percent / _verses.Count());
        }
    }
    public void HideNumber(int number)
    {
        foreach (Verse verse in _verses)
        {
            verse.HideNumber(number);
        }
    }
    public string Serialize()
    {
        string newString = $"{_book} chapter {_chapterNumber.ToString()}: {VerseRangeString()}\n";
        for (int verse = 0; verse < _verses.Count(); verse++)
        {
            newString = newString + _verses[verse].Serialize() + "\n";
        }
        return newString;
    }
    private string VerseRangeString()
    {
        return _verses[0].VerseNumber.ToString() + "-" + _verses[_verses.Count - 1].VerseNumber.ToString();
    }



}