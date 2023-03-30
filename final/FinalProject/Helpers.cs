public static class Helpers
{
    static public int ReadInt()
    {
        int val;
        while(!int.TryParse(Console.ReadLine(),out val)){
            Console.Write("Invalid input. Try again:");
        }
        return val;
    }
    static public string WrapText(string sourceText, int targetLength)
    {
        string wrappedText = "";
        string[] splitString = sourceText.Split(" ");
        for(int word = 0; word < splitString.Count(); word++)
        {
            if((wrappedText+splitString[word]).Length > targetLength)
            {
                wrappedText += $"\n{splitString[word]} ";
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
}