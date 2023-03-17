public class TextImage
{
    private string[] _textArray;
    private int _width, _height;
    public TextImage(int width, int height)
    {
        _width = width;
        _height = height;
        _textArray = new string[_height];
        for (int y = 0; y < _height; y++)
        {
            _textArray[y] = "".PadLeft(_width, ' ');
        }
    }
    public void Draw(string text, int x, int y)
    {
        string[] splitText = text.Split("\n");
        int ty = y; //Target y index
        for (int sy = 0; sy < (splitText.Count() <= _height ? splitText.Count() : _height); sy++) //Source y index
        {
            if (ty > _textArray.Count()) //Break if target x exceeds target height
                break;
            if (ty >= 0 && ty < _textArray.Count()) //Only draw if index in range
            {
                if (x < _width) //If it doesn't start outside width
                {
                    //_textArray[ty] = _textArray[ty].Remove(x, splitText[sy].Length); //take out
                    _textArray[ty] = _textArray[ty].Remove(x, (splitText[sy].Length + x < _width ? splitText[sy].Length : _width - x));
                    _textArray[ty] = _textArray[ty].Insert(x, splitText[sy]); //put in
                    if (_textArray[ty].Length > _width)
                    {
                        _textArray[ty] = _textArray[ty].Substring(0, _width);
                    }
                }
            }
            ty++;
        }
    }
    public void DrawCard(int x, int y, int width, int height, Boolean stack = false)
    {
        string img = "";
        for (int ly = y; ly < y + height + 1; ly++)
        {
            if (ly == y)
            {
                //fist line
                img += $"╭{"".PadLeft(width - 2, '─')}╮\n";
            }
            else if (ly == y + height - 1)
            {
                //last line
                img += $"╰{"".PadLeft(width - 2, '─')}╯";
                if (stack)
                {
                    img += "│\n";
                }
                else
                {
                    img += "\n";
                }
            }
            else if (ly == y + height)
            {
                //stack effect on bottom
                if(stack)
                {
                img += $" ╰{"".PadLeft(width - 2, '─')}╯";
                }
            }
            else
            {
                //middle lines
                img += $"│{"".PadLeft(width - 2, ' ')}";
                if (stack)
                {
                    if (ly == y + 1)
                    {
                        img += "├╮\n";
                    }
                    else
                    {
                        img += "││\n";
                    }
                }
                else
                {
                    img += "│\n";
                }
            }
        }
        Draw(img, x, y);
    }
    public string GetString()
    {
        return string.Join("\n", _textArray);
    }
}