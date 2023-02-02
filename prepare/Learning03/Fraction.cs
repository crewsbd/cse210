public class Fraction
{
    private int _top, _bottom;
    //Constructors
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }
    //Getters and Setters
    public int Top
    {
        get => _top;
        set => _top = value;
    }
    public int Bottom
    {
        get => _bottom;
        set => _bottom = value;
    }
    //Return a string representation of the fraction
    public string GetFractionString()
    {
            return  _top.ToString() + "/" + _bottom.ToString();
    }
    //Return the double decimal value of the fraction
    public double GetDecimalValue()
    {
            return (double)_top/_bottom;
    }
}