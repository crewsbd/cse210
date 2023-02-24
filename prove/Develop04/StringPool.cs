
public class StringPool 
{
    private List<string> _pool;
    private List<int> _unused;
    private Random _randomNumber;

    public StringPool()
    {
        _pool = new List<string>();
        _unused = new List<int>();
        _randomNumber = new Random(DateTime.Now.Millisecond);
    }
    public StringPool(string[] newStringArray)
    {
        _pool = new List<string>(newStringArray);
        ResetUnused();
    }

    public string getRandom()
    {
        return "";
    }
    private void ResetUnused()
    {
        
        _unused.Clear();
        for(int i = 0; i <= _pool.Count(); i++)
        {
            _unused.Add(i);
        }
    }
}