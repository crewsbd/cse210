using System;

class Program
{
    static void Main(string[] args)
    {
        /*Console.WriteLine("What is the magic number? ");
        int magicNumber = int.Parse(Console.ReadLine()); */
        Random generator = new Random();
        int magicNumber = 0;
        int guess = -1;
        int guesses = 0;
        string answer = "";

        do
        {
            magicNumber = generator.Next(1, 100); //reset these numbers
            guesses = 0;
            do
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guesses++;

                if(guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if(guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
            } while(magicNumber != guess);
        Console.WriteLine($"You guessed it!\nIt took you {guesses} guesses.");

        Console.Write("Do you want to play again? ");
        answer = Console.ReadLine();
        } while(answer.ToLower() == "yes");
    }
}