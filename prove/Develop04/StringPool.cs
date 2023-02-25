
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
        ResetUnused();
    }
    public StringPool(string[] newStringArray)
    {
        _pool = new List<string>(newStringArray);
        _unused = new List<int>();
        _randomNumber = new Random(DateTime.Now.Millisecond);
        ResetUnused();
    }

    public string GetRandom()
    {
        if(_unused.Count == 0)
        {
            ResetUnused();
            //Console.WriteLine("RESET");
        }
        int unusedIndex = _randomNumber.Next(_unused.Count);
        int poolIndex = _unused[unusedIndex];
        _unused.RemoveAt(unusedIndex);

        return _pool[poolIndex];
    }
    public void Add(string newString)
    {
        _pool.Add(newString);
        _unused.Add(_pool.IndexOf(newString));
    }
    private void ResetUnused()
    {
        _unused.Clear();
        for(int i = 0; i < _pool.Count(); i++)
        {
            _unused.Add(i);
        }
    }
}