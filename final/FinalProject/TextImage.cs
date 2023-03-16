public class TextImage
{
    private string[] _textArray;
    public TextImage(string imageString)
    {
        _textArray = imageString.Split("\n");
    }
    public void Draw(string text, int x, int y)
    {
        string[] splitText = text.Split("\n");
        int ty = y; //Target y index
        for (int sy = 0; sy < splitText.Count(); sy++) //Source y index
        {
            if (ty > _textArray.Count()) //Break if target x exceeds target height
                break;
            if (ty >= 0)
            {
                int firstLength = x;
                int secondLength = _textArray[ty].Length - (x + splitText[sy].Length);

                _textArray[ty] = _textArray[ty].Substring(0, firstLength) + splitText[sy] + _textArray[ty].Substring(x + splitText[sy].Length, secondLength);
            }
            ty++;
        }
    }
    public string GetString()
    {
        return string.Join("\n", _textArray);
    }
}