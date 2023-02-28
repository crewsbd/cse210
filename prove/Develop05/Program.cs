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
    static void Main(string[] args)
    {
        Boolean continueLoop = true;
        string option = "";
        do
        {
            Console.Write("Menu Options\n1. Create New Goal\n2. List Goals\n3.Save Goals\n4.Load Goals\n5. Record Event\n6. Quit\nSelect a choice from the menu: ");
            option = Console.ReadLine();
            switch (option)
            {
                case "1":
                { 
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
        while(continueLoop);
    }
}