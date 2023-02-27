public class Square : Shape
{
    private double _side;

    public Square(string newColor, double newSide) : base(newColor)
    {
        _side = newSide;
    }
    public override double GetArea()
    {
        return Math.Pow(_side, 2);
    }
}