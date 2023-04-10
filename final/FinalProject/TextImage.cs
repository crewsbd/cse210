public class TextImage
{
    private char[,] _charArray;
    private int _width, _height;
    private int _termY;
    public TextImage(int width, int height)
    {
        _width = width;
        _height = height;
        _charArray = new char[_width, _height];
        _termY = 0;
        Clear();
    }
    public void DrawChar(char character, int x, int y)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            _charArray[x, y] = character;
        }
    }
    public void DrawTextLine(string textLine, int x, int y)
    {
        if (y >= 0 && y < _height)
        {
            textLine = textLine.Replace('\n', ' ');
            for (int ix = 0; ix < textLine.Length; ix++)
            {
                if (ix >= 0 && ix < _width)
                {
                    _charArray[ix + x, y] = textLine[ix];
                }
            }
        }
    }
    public void DrawVerticalLine(char character, int x, int y, int length)
    {
        if (x >= 0 && x < _width && length > 0)
        {
            for (int iy = y; iy < length + y; iy++)
            {
                if (iy >= 0 && iy < _height)
                {
                    _charArray[x, iy] = character;
                }
            }
        }
    }
    public void DrawHorizontalLine(char character, int x, int y, int length)
    {
        if (y >= 0 && y < _height && length > 0)
        {
            for (int ix = x; ix < length + x; ix++)
            {
                if (ix >= 0 && ix < _width)
                {
                    _charArray[ix, y] = character;
                }
            }
        }
    }
    public void DrawCard(int x, int y, int width, int height, Boolean stack = false)
    {
        for (int iy = 1; iy < height - 1; iy++)
        {
            for (int ix = 1; ix < width - 1; ix++)
            {
                DrawChar(' ', ix + x, iy + y);
            }
        }
        DrawHorizontalLine('─', x + 1, y, width - 2);  //top
        DrawHorizontalLine('─', x + 1, y + height - 1, width - 2); //bottom
        DrawVerticalLine('│', x, y + 1, height - 2); //left
        DrawVerticalLine('│', x + width - 1, y + 1, height - 2); //right
        DrawChar('╭', x, y);
        DrawChar('╮', x + width - 1, y);
        DrawChar('╰', x, y + height - 1);
        DrawChar('╯', x + width - 1, y + height - 1);
    }
    public void DrawHighlight(int x, int y, int width, int height, Boolean stack = false)
    {
        DrawHorizontalLine('═', x + 1, y, width - 2);  //top
        DrawHorizontalLine('═', x + 1, y + height - 1, width - 2); //bottom
        DrawVerticalLine('║', x, y + 1, height - 2); //left
        DrawVerticalLine('║', x + width - 1, y + 1, height - 2); //right
        DrawChar('╔', x, y);
        DrawChar('╗', x + width - 1, y);
        DrawChar('╚', x, y + height - 1);
        DrawChar('╝', x + width - 1, y + height - 1);
    }
    public void Draw(string text, int x, int y)
    {
        if (x < _width && x + text.Length >= 0)
        {
            string[] splitString = text.Split('\n');
            for (int line = 0; line < splitString.Count(); line++)
            {
                if (line >= 0 && line < _height)
                {
                    DrawTextLine(splitString[line], x, line + y);
                }
            }
        }
    }
    public void Draw(TextImage image, int x, int y)
    {
        Draw(image.GetString(), x, y);
    }
    public void TermWrite(string line)
    {
        string[] lines = line.Split('\n');
        for (int ctr = 0; ctr < lines.Count(); ctr++)
        {
            if (_termY >= _height - 1)
            {
                this.Draw(this.GetString(), 0, -1); //move up a line
                _termY = _height - 1;
            }
            this.DrawTextLine(lines[ctr], 0, _termY);
            _termY++;

        }
    }
    public string GetString()
    {
        string returnString = "";
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                returnString += _charArray[x, y];
            }
            returnString += "\n";
        }
        return returnString.Substring(0, returnString.Length - 1);

    }
    public void Clear()
    {
        _termY = 0;
        _charArray = new char[_width, _height];
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _charArray[x, y] = ' ';
            }
        }
    }
}