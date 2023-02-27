public class Circle : Shape
{
    private double _radius;

    public Circle(string _newColor, double newRadius) : base(_newColor)
    {
        _radius = newRadius;
    }
    public override double GetArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }
}