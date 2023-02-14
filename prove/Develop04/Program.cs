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
    string menuString = "Menu Options:\n   1. Start breathing activity\n" +
        "   2. Start relecting activity\n" +
        "   3. Start listing Activity" +
        "   4. Quit";
    string prompt = "Select a choice from the menu";
    static void Main(string[] args)
    {

        Console.WriteLine("Test animate:\n");
        BreathingActivity ba = new BreathingActivity();
        ba.Launch();
    }
}