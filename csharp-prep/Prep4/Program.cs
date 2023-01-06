using System;
using System.Collections.Generic;

class Program
{

    static void Main(string[] args)
    {
        int userInput = 0;
        List<int> numberList = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            Console.Write("Enter number: ");
            userInput = int.Parse(Console.ReadLine());
            if(userInput != 0)
            {
                numberList.Add(userInput);
            }
            else
            {
                break;
            }
        }
        while(true);

        Console.WriteLine($"The sum is: {numberList.Sum()}");
        Console.WriteLine($"The average is: {(float)numberList.Sum()/numberList.Count}");
        Console.WriteLine($"The largest number is: {numberList.Max()}");
        Console.WriteLine($"The number with the least magnitude is: {numberList.MinBy((theNumber)=> Math.Abs(theNumber))}");
        numberList.Sort();
        Console.Write("Numbers in ascending order: ");
        for(int ctr = 0; ctr < numberList.Count; ctr++)
        {
            if(ctr != 0)
            {
                Console.Write(",");
            }
            Console.Write($" {numberList[ctr]}");
        }
        Console.WriteLine("");
    }
}