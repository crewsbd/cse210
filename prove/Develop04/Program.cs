/*
- Menu(of activities)
- Each activity has a start message: Name, Description, Duration prompt(seconds) -> pause for several seconds -> start.
- Common ending message("Good job") -> Pause -> State activity duration -> pause -> finish
- Pause is animated \|/-, > -> --> ->
- See video
- Actities (classes?): reflection, breathing and enumeration. See doc

DON'T
- Track stats
- Track history(prompts etc.)
*/
using System;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();
        activities.Add(new BreathingActivity());
        activities.Add(new ReflectionActivity());
        activities.Add(new ListingActivity());
        Boolean continueLoop = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            for (int i = 0; i < activities.Count; i++)
            {
                Console.WriteLine("  " + (i + 1).ToString() + ". Start " + activities[i].Name);
            }

            Console.WriteLine($"  {activities.Count + 1}. Quit\nSelect a choice from the menu:");
            int responseNumber = 0;
            Boolean parsed = int.TryParse(Console.ReadLine(), out responseNumber);
            if (responseNumber != activities.Count + 1 && parsed && responseNumber < activities.Count + 1)
            {
                activities[responseNumber - 1].Launch();
            }
            else if (responseNumber == activities.Count + 1)
            {
                continueLoop = false;
            }

        } while (continueLoop);
    }
}