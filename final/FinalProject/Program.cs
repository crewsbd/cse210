using System;

class Program
{
    static void Main(string[] args)
    {
        string box = @"|--------------------------------|
| 2                              |
| 3                              |
| 4                              |
| 5                              |
| 6                              |
| 7                              |
| 8                              |
| 9                              |
| 10                             |
| 11                             |
| 12                             |
|--------------------------------|";
        TextImage buffer = new TextImage(box);
       
        TextImage image = new TextImage(@"/---\
|   |
\---/");

        for(int ctr = 0; ctr < 10; ctr++)
        {
            Thread.Sleep(75);
            buffer.Draw(box,0,0);
            buffer.Draw(image.GetString(), ctr, ctr );
            Console.Clear();
            Console.WriteLine(buffer.GetString());

        }
        
    }
}