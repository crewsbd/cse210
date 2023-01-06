using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        string stringResult = PromptUserName();
        int intResult = PromptUserNumber();
        int squaredResult = SquareNumber(intResult);
        DisplayResult(stringResult, squaredResult);

    }
    static void DisplayWelcome()
    {

    }
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }
    static int SquareNumber(int aNumber)
    {
        return (int)Math.Pow(aNumber, 2); //really?
    }
    static void DisplayResult(string userName, int userNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {userNumber}");
    }
}