using System;

class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 80;
        const int screenHeight = 24;
        string logo = File.ReadAllText("Resources/title.txt");
        
        
        
        TextImage buffer = new TextImage(screenWidth, screenHeight);
        TextImage image = new TextImage(5, 4);

        
        buffer.DrawCard(0,0,screenWidth, screenHeight);
        buffer.Draw("Make sure this box fits your screen\nPress any key to continue.", 20, screenHeight/2);
        Console.Clear();

        Console.WriteLine(buffer.GetString());
        Console.ReadKey();

        image.DrawCard(0,0,5,4);
        buffer.Clear();
        buffer.Draw(logo, 0,5);
        Console.Clear();
       
        Console.WriteLine(buffer.GetString());
        Thread.Sleep(5000);


        do
        {
            
            int cardWidth = 20;
            int cardHeight = 7;

            for (int ctr = 0; ctr < 30; ctr++)
            {
                Thread.Sleep(75);
                buffer.Clear();
                buffer.DrawCard(0, 0, 70, 23, false); //clear buffer
                for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    buffer.DrawCard(x * cardWidth + 1, y * cardHeight + 1, cardWidth, cardHeight, false);
                }
            }
                //buffer.Draw(box, 0, 0);
                
                //buffer.Draw(image.GetString(), ctr, ctr);
                buffer.DrawCard(ctr, 4, 5, 4, false);
                buffer.Draw(image.GetString(), ctr, ctr);
                Console.Clear();
                Console.WriteLine(buffer.GetString());

            }
        } while (true); //Just testing. 

    }
}