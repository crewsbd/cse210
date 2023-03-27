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
<<<<<<< HEAD
    static public string WrapText(string sourceText, int targetLength)
    {
        string wrappedText = "";
        string[] splitString = sourceText.Split(" ");
        for(int word = 0; word < splitString.Count(); word++)
        {
            if((wrappedText+splitString[word]).Length > targetLength)
            {
                wrappedText += $"\n{splitString[word] }";
            }
            else
            {
            wrappedText += $"\n{splitString[word] }";
            }
        }
        return wrappedText;
    }
=======
>>>>>>> 1885891f413add876a2ea430121bb5c7491fd091
}