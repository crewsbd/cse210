using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("");

        Resume myResume = new Resume("Brian Crews");
        myResume.AddJob("BYUI", "Master of All", 1995, 2023);
        
        Job job2 = new Job();
        job2.Company = "Peach";
        job2.JobTitle = "Super Coder";
        job2.StartYear = 1920;
        job2.EndYear = 2512;
        myResume.AddJob(job2);

        myResume.AddJob(new Job("Gubmint", "Auditor", 0, 5000));

        myResume.Display();
        
        Console.WriteLine("");
    }
}