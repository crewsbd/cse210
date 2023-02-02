using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction fraction1 = new Fraction(); //     1/1
        Fraction fraction2 = new Fraction(5); //    5/1
        Fraction fraction3 = new Fraction(5, 3); // 5/3

        fraction1.Bottom = 2;
        Console.WriteLine($"Get Set bottom = 2: {fraction1.Bottom}");
        fraction2.Top = 5;
        Console.WriteLine($"Get Set top = 5: {fraction2.Top}");
        Console.WriteLine($"fraction1 String: {fraction1.GetFractionString()} Decimal: {fraction1.GetDecimalValue()}");
        Console.WriteLine($"fraction2 String: {fraction2.GetFractionString()} Decimal: {fraction2.GetDecimalValue()}");
        Console.WriteLine($"fraction3 String: {fraction3.GetFractionString()} Decimal: {fraction3.GetDecimalValue()}");
    }
}