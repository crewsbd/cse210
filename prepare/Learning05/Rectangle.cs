public class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(string _newColor, double newLength, double newWidth) : base(_newColor)
    {
        _length = newLength;
        _width = newWidth;
    }
    public override double GetArea()
    {
        return _length * _width;
    }
}