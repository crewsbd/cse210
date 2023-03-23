using System;

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
        int players = int.Parse(Console.ReadLine());
        for (int ctr = 0; ctr < players; ctr++)
        {
            Console.Write($"Player {ctr + 1} name? ");
            string name = Console.ReadLine();
            table.AddPlayer(name);
        }
        table.StartGame();




        /*
               do
               {

                   int cardWidth = 13;
                   int cardHeight = 5;

                   for (int ctr = 0; ctr < 30; ctr++)
                   {
                       Thread.Sleep(75);
                       buffer.Clear();
                       buffer.DrawCard(0, 0, 80, 17, false); //clear buffer
                       for (int x = 0; x < 3; x++)
                       {
                           for (int y = 0; y < 3; y++)
                           {
                               buffer.DrawCard(x * (cardWidth+1) + 1, y * cardHeight + 1, cardWidth, cardHeight, false);
                           }

                       }
                       for (int x = 0; x < 5; x++)
                       {
                           buffer.DrawCard(x * 10, 17, 9, 7);
                       }

                       buffer.DrawCard(ctr, 4, 5, 4, false);
                       buffer.Draw(image.GetString(), ctr, ctr);
                       Console.Clear();
                       Console.WriteLine(buffer.GetString());


                   }
               } while (true); //Just testing.   */

    }
}