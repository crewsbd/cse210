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
    static List<Goal> history = new List<Goal>();
    static int historySize = 3;
    static string userName;
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("What is your name? ");
        userName = Console.ReadLine();
        Boolean continueLoop = true;
        string option = "";
        //Main program loop-----------
        do
        {
            Console.Clear();
            Console.WriteLine($"--==| {userName}s Goals |==--");

            if (history.Count > 0)
            {
                Console.WriteLine("Recent activity");
                foreach (Goal goal in history)
                {
                    Console.WriteLine(goal.Serialize());
                }
            }
            Console.WriteLine($"Total Points: {TotalPoints()}");
            Console.Write("Menu Options\n1. Create New Goal\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Event\n6. Quit\nSelect a choice from the menu: ");
            option = Console.ReadLine();
            Console.WriteLine();
            switch (option)
            {
                case "1":
                    {
                        CreateGoal();
                        AnyKey();
                        break;
                    }
                case "2":
                    {
                        ListGoals(showComplete: true);
                        AnyKey();
                        break;
                    }
                case "3":
                    {
                        SaveGoals();
                        AnyKey();
                        break;
                    }
                case "4":
                    {
                        LoadGoals();
                        AnyKey();
                        break;
                    }
                case "5":
                    {
                        RecordEvent();
                        AnyKey();
                        break;
                    }
                case "6":
                    {   //quit
                        continueLoop = false;
                        break;
                    }
            }
        }
        while (continueLoop);
    }
    //Menu functions--------------------------------
    static void CreateGoal()
    {
        Console.Write("The types of goals are:\n1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\nWhich type of goal would you like to create? ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                {
                    Console.Write("What is the name of your simple goal? ");
                    string name = Console.ReadLine();
                    Console.Write("What is a short description of your goal? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated with this goal? ");
                    int points = ReadInt();

                    goals.Add(new SimpleGoal(name, description, points));
                    break;
                }
            case "2":
                {
                    Console.Write("What is the name of your eternal goal? ");
                    string name = Console.ReadLine();
                    Console.Write("What is a short description of your goal? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated with this goal? ");
                    int points = ReadInt();

                    goals.Add(new EternalGoal(name, description, points));
                    break;
                }
            case "3":
                {
                    Console.Write("What is the name of your checklist goal? ");
                    string name = Console.ReadLine();
                    Console.Write("What is a short description of your goal? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated one completion of this goal? ");
                    int points = ReadInt();
                    Console.Write("How many times must this goal be completed? ");
                    int completions = ReadInt();
                    Console.Write("How many points for a full completion of this goal? ");
                    int bonus = ReadInt();

                    goals.Add(new ChecklistGoal(name, description, points, completions, bonus));
                    break;
                }
            default:
                {
                    Console.WriteLine("Unknown option");
                    break;
                }
        }
    }
    static List<Goal> ListGoals(Boolean numbered = false, Boolean showComplete = false)
    {
        List<Goal> incompleteGoals = goals.FindAll((goal) =>
        {
            if (!goal.IsComplete())
            {
                return true;
            }
            else
            {
                if (showComplete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        });

        int num = 0;
        foreach (Goal goal in incompleteGoals)
        {
            Console.WriteLine($"{(numbered ? num + " " : "")}{goal.Serialize()}");
            num++;

        }

        return incompleteGoals;
    }
    static void SaveGoals()
    {
        SaveData saveData = new SaveData(userName, goals);
        saveData.Save();
    }
    static void LoadGoals()
    {
        SaveData saveData = new SaveData(userName, goals);
        saveData.Load();
    }
    static void RecordEvent()
    {
        List<Goal> incompleteGoals = ListGoals(true, false);
        Console.Write("Which goal? ");
        int goal = ReadInt();
        incompleteGoals[goal].RecordEvent();
        history.Add(incompleteGoals[goal]);
        if (history.Count > historySize)
        {
            history.RemoveAt(0); //Pipe
        }
    }
    //Helpers--------------------------
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
    static void AnyKey()
    {
        Console.WriteLine("Press a key.");
        Console.ReadLine();
    }
    static int TotalPoints()
    {
        int totalPoints = 0;
        foreach (Goal goal in goals)
        {
            totalPoints += goal.GetTotalPoints();
        }
        return totalPoints;
    }
}