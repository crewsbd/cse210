class Reservoir
{
    private int _current;
    private int _max;
    private int _boost;

    public Reservoir(int initial, int max)
    {
        _current = initial;
        _max = max;
        Add(0); //easy check that max isnt exceeded
    }
    public int Base
    {
        get { return _current;}
    }
    public int Value
    {
        get { return _current + _boost; }
    }
    public int Max
    {
        get { return _max; }
    }
    public void Add(int value)
    {
        _current += value;
        if (_current >= _max)
        {
            _current = _max;
        }
    }
    public void Subtract(int value)
    {
        if(_boost > 0)
        {
            _boost -= value;
            if(_boost < 0)
            {
                _current += _boost;
                _boost = 0;
            }
        }
        else
        {
            _current -= value;
        }
        if(_current < 0)
        {
            _current = 0;
        }
    }
    public void Boost(int value)
    {
        _boost = value;
    }
    public void ClearBoost()
    {
        _boost = 0;
    }
}