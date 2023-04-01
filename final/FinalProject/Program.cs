using System;

/*todo
    X Correct negative damage
    X Traps
    X Boss
    X Boss endgame!! At least this.
    X Graphical tweaks
    Word wrap improvement.  Get a line counter so messages box heights fit.
    put names in more places.
    Show health during fight
    Put fight in the "console" instead of notify.
    X Make deck bigger
    X Make more cards
    X Add a help spash screen
    Make getters/setters consistent
    X Card images
    X Tune/Balance game difficulty
Wish list
    Item slots? Hand, Head, Feet etc...
    Stack potions
    FINE tune game difficulty
    Hand limit?
    Put settings in a json file
*/

class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 80;
        const int screenHeight = 24;
        string logo = File.ReadAllText("Resources/title.txt");
        string help = File.ReadAllText("Resources/Help.txt");


        Table table = new Table(80, 24);

        TextImage buffer = new TextImage(screenWidth, screenHeight);
        TextImage image = new TextImage(5, 4);
        Console.Clear();

        buffer.DrawCard(0, 0, screenWidth, screenHeight);
        buffer.Draw("Make sure this box fits your screen\n    Press any key to continue.", 22, screenHeight / 2 - 2);

        Console.SetCursorPosition(0, 0);
        Console.WriteLine(buffer.GetString());
        Console.ReadKey();

        buffer.Clear();
        buffer.DrawCard(0, 0, screenWidth, screenHeight);
        buffer.Draw(help, 2, 1);
        buffer.Draw("Press any key to continue.", 25, screenHeight - 2);
        Console.SetCursorPosition(0, 0);
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
        //int players = Helpers.ReadInt();
        int players = Helpers.ReadIntRange(2,4);
        for (int ctr = 0; ctr < players; ctr++)
        {
            Console.Write($"Player {ctr + 1} name? ");
            string name = Console.ReadLine();
            table.AddPlayer(name);
        }
        table.StartGame();
    }
}