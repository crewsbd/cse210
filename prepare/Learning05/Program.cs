using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Square("Red", 5));
        shapes.Add(new Rectangle("Blue", 2, 5));
        shapes.Add(new Circle("Pink", 5));

        shapes.ForEach((shape) =>
        {
            Console.WriteLine($"Color: {shape.GetColor()} Area: {double.Round(shape.GetArea(), 2)}");
        });

        //Square testSquare = new Square("RED", 5);
        //Console.WriteLine($"Color :{testSquare.GetColor()} Area:{testSquare.GetArea()}");
    }
}