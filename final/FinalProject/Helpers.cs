public static class Helpers
{
    static public int ReadInt()
    {
        int val;
        while (!int.TryParse(Console.ReadLine(), out val))
        {
            Console.Write("Invalid input. Try again:");
        }
        return val;
    }
    static public string WrapText(string sourceText, int targetLength)
    {
        string wrappedText = "";
        string[] splitString = sourceText.Split(" ");
        int lengthTracker = 0;

        for (int word = 0; word < splitString.Count(); word++)
        {
            lengthTracker += splitString[word].Length+1;
            if (lengthTracker > targetLength)
            {
                wrappedText += $"\n{splitString[word]} ";
                lengthTracker = 0;
            }
            else
            {
                wrappedText += $"{splitString[word]} ";
            }
        }
        return wrappedText;
    }
    static public Encounter ListPop(List<Encounter> deck)
    {
        Encounter popped = deck[0];
        deck.RemoveAt(0);
        return popped;
    }
    static public void Notify(string msg, TextImage screen)
    {
        string backup = screen.GetString();
        screen.DrawCard(20, 8, 40, 4);
        screen.Draw($"{msg}\nPress any key.", 21, 9);
        Console.SetCursorPosition(0, 0);
        Console.Write(screen.GetString());
        screen.Draw(backup, 0, 0);
        Console.ReadKey();
    }
    static public int LineCount(string inputString)
    {
        return inputString.Split("\n").Count();
        
    } 
}