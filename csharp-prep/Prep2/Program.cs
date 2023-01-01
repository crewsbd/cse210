using System;

class Program
{
    static void Main(string[] args)
    {
        //Get grade percentage from the user and convert it to an integer
        Console.Write("Grade percentage: ");
        string stringGradePercentage = Console.ReadLine();
        int intGradePercentage = (int)float.Floor(float.Parse(stringGradePercentage));
        int gradeLastDigit = intGradePercentage % 10;
        string letter = "F";
     
        //Determine letter grade
        if(intGradePercentage >= 90)
        {
            letter = "A";
        }
        else if(intGradePercentage >= 80)
        {
            letter = "B";
        }
        else if(intGradePercentage >= 70)
        {
            letter = "C";
        }
        else if(intGradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        //Add letter grade modifier
        if(gradeLastDigit >= 7 && intGradePercentage < 90 && intGradePercentage >= 60)
        {
            letter = letter + "+";
        }
        else if(gradeLastDigit < 3 && intGradePercentage >= 60)
        {
            letter = letter + "-";
        }

        //Print letter grade
        Console.WriteLine($"Grade: {letter}");

        //Print feedback
        if(intGradePercentage >= 70)
        {
            Console.WriteLine("You are to be congratulated on passing this course.");
        }
        else
        {
            Console.WriteLine("We regret to inform you that you have not passed this class. Keep working hard and you'll get it next time.");
        }

    }
}