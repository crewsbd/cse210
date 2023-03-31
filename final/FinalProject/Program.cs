using System;

/*todo
    Correct negative damage
    Traps
    Boss
    Graphical tweaks
    put names in more places.
    Make deck bigger
    Make more cards
*/

class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 80;
        const int screenHeight = 24;
        string logo = File.ReadAllText("Resources/title.txt");


        Table table = new Table(80, 24);

        TextImage buffer = new TextImage(screenWidth, screenHeight);
        TextImage image = new TextImage(5, 4);


        buffer.DrawCard(0, 0, screenWidth, screenHeight);
        buffer.Draw("Make sure this box fits your screen\n    Press any key to continue.", 20, screenHeight / 2);
        Console.Clear();

        Console.WriteLine(buffer.GetString());
        Console.ReadKey();

        image.DrawCard(0, 0, 5, 4);
        buffer.Clear();
        buffer.Draw(logo, 0, 5);
        Console.Clear();

        Console.WriteLine(buffer.GetString());
        //Thread.Sleep(5000);
        Console.SetCursorPosition(0, 15);
        Console.CursorVisible = true;
        Console.Write("How many players? ");
        int players = Helpers.ReadInt();
        for (int ctr = 0; ctr < players; ctr++)
        {
            Console.Write($"Player {ctr + 1} name? ");
            string name = Console.ReadLine();
            table.AddPlayer(name);
        }
        table.StartGame();
    }
}