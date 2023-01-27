static class Boxit
{
    static public string ShadowBox(string text, int width = 0, string title = null)
    {
        //Need a max width...on account of word wrapping.
        string boxedString = "";
        int maxWidth = 0;

        if(width > 0) //Wrap text if a width is specified.
        {
            text = _wrap(text, width);
        }
        string[] lines = text.Split("\n");
        foreach(string line in lines)
        {
            if(line.Length > maxWidth)
            {
                maxWidth = line.Length;
            }
        }
        if(maxWidth < width && width > 0)
        {
            maxWidth = width;
        }
        if(title != null)
        {
            if(title.Length + 2 > maxWidth)
            {
                maxWidth = title.Length+4;
            }
        }
        boxedString += $"╭{"".PadLeft(maxWidth + 2, '─')}┒\n";
        if(title != null)
        {
            boxedString = boxedString.Remove(3,title.Length+4).Insert(3, $"┤ {title} ├");
        }
        foreach(string line in lines)
        {
            boxedString += $"│ {line}{"".PadLeft(maxWidth - line.Length,' ')} ┃\n";
        }
        boxedString += $"┕{"".PadLeft(maxWidth + 2, '━')}┛";

        return boxedString;
    }
    static private string _wrap(string text, int width)
    {
        string wrappedParagraph = "";
        foreach(string line in text.Split("\n"))
        {
            int maxWordLength = 0;
            string[] words = line.Split(" ");
            foreach(string word in words)
            {
                if(word.Length > maxWordLength)
                {
                    maxWordLength = word.Length;
                }
            }
            if(maxWordLength > width)
            {
                width = maxWordLength; //Grow to make it work
            }
            int currentLineLength = 0;
            string wrappedLine = "";
            foreach(string word in words)
            {
                if(currentLineLength + word.Length + 2 >= width)
                {   //Add a carriage return
                    //Console.Write($"{currentLineLength} ");
                    wrappedLine += $"\n{word} ";
                    currentLineLength = word.Length+1;
                }
                else //No carriage return
                {
                    wrappedLine += $"{word} ";
                    currentLineLength += word.Length+1;
                }
            }
            wrappedParagraph += $"{wrappedLine}\n";
        }
        
        
        return wrappedParagraph;
    }
}