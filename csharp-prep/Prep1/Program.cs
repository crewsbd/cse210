using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");

        /*Console.Write("What is your name? ");
        string name = Console.ReadLine();
        string[] names = name.Split(" ");
        if(names.Length >= 2)
        {
            Console.WriteLine($"Your name is {names[1]}, {names[0]} {names[1]}.");
        }
        else
        {
            Console.WriteLine($"Hello {name}.");
        } */
    }
}