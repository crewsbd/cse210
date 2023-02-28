/*
Full credit ideas
--Show the last X events in menu.
--Reward displays for milestones etc.
*/

/*
Create New Goal
    Simple Goal
        What is the name of your goal
        What is a short description of it?
        What is the amount of points associated with this goal?
    Eternal Goal (never complete)
        Name
        Desc
        Points
    Checklist Goal
        Name
        Desc
        Points(per check)
        Number of checks
        Points Bonus
List Goals
    "The Goals Are"
    LIST OF GOALS
    You have X points
Save Goals
    Save to a file.
    csv or object serialization?
Load Goals
    Load from a file
Record Event
Quit
*/


using System;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static void Main(string[] args)
    {
        Boolean continueLoop = true;
        string option = "";
        do
        {
            Console.Write("Menu Options\n1. Create New Goal\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Event\n6. Quit\nSelect a choice from the menu: ");
            option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    {
                        CreateGoal();
                        break;
                    }
                case "2":
                    {
                        ListGoals();
                        break;
                    }
                case "3":
                    {
                        break;
                    }
                case "4":
                    {
                        break;
                    }
                case "5":
                    {
                        break;
                    }
                case "6":
                    {
                        continueLoop = false;
                        break;
                    }
            }
        }
        while (continueLoop);
    }
    static void CreateGoal()
    {
        Console.Write("The types of goals are:\n1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\nWhich type of goal would you like to create? ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                {
                    Console.Write("What is the name of your goal? ");
                    string name = Console.ReadLine();
                    Console.Write("What is a short description of your goal? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated with this goal?");
                    int points = ReadInt();

                    goals.Add(new SimpleGoal(name, description, points));
                    break;
                }
            case "2":
                {
                    break;
                }
            case "3":
                {
                    break;
                }
            default:
                {
                    Console.WriteLine("Unknown option");
                    break;
                }
        }
    }
    static void ListGoals()
    {
        foreach(Goal goal in goals)
        {
            Console.WriteLine(goal.Serialize());
        }
    }
    static int ReadInt()
    {
        int value;
        do
        {
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Not an integer.");
            }
        } while (true);
    }
}