using System;

class Program
{
    static void Main(string[] argentavis)
    {
        Assignment assignment1 = new Assignment("Brian", "Computer Science");
        Console.WriteLine(assignment1.GetSummary());

        MathAssignment assignment2 = new MathAssignment("Liam", "Hard Math for Smart People","2B", "A.A^2+B^2=C^2");
        Console.WriteLine(assignment2.GetSummary());
        Console.WriteLine(assignment2.GetHomeworkList());

        WritingAssignment assignment3 = new WritingAssignment("Ian", "Writing the Best Book 101", "Profound Words");
        Console.WriteLine(assignment3.GetSummary());
        Console.WriteLine(assignment3.GetWritingInformation());
    }
}