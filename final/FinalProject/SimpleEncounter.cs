using System.Text.Json;
class SimpleEncounter : Encounter
{
    public SimpleEncounter(JsonElement cardData, Item[] items) : base(cardData, items)
    {

    }
    public override Boolean Run(Player player, TextImage screen)
    {
        string originalBuffer = screen.GetString();
        screen.DrawCard(2, 2, 40, 20);
        screen.Draw($"You attack the {_name} ⤷", 3, 3);
        Update(screen);
        Console.ReadKey();
        if (player.Items.Count() > 0) //Begin item select loop
        {
            screen.Draw("Pick the cards you will use.", 3, 4);  //Need to filter this list to only one-use cards
            Update(screen);
            int focusedCard = 0; //Which card we looking at?
            Boolean[] selectedCards = new Boolean[player.Items.Count()]; //track selected card
            Array.Fill(selectedCards, false);
            Boolean selectingCards = true; //keep looping?
            string backgroundBackup = screen.GetString();
            do
            {
                screen.Draw(backgroundBackup, 0, 0);
                for (int c = 0; c < player.Items.Count(); c++)
                {
                    screen.DrawCard(c * 9 + 1, 16, 9, 7);
                    if (selectedCards[c])
                    {
                        screen.Draw("-X-", c * 9 + 2, 17);
                    }
                }
                screen.DrawCard(focusedCard * 9, 15, 11, 9);
                if (selectedCards[focusedCard])
                {
                    screen.Draw("-X-", focusedCard * 9 + 1, 16);
                }

                Update(screen);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    focusedCard--;
                    if (focusedCard < 0)
                    {
                        focusedCard = player.Items.Count() - 1;
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    focusedCard = (focusedCard + 1) % (player.Items.Count());
                }
                else if (key == ConsoleKey.Enter)
                {
                    //Toggle the card
                    selectedCards[focusedCard] = !selectedCards[focusedCard];
                }
            } while (selectingCards);
        }

        //Temp debug. This needs to be on condition of success.
        player.GiveItems(_rewards);
        return true;
    }
    public override Boolean Reject(Player player, TextImage screen)
    {
        return true;
    }
    public override Item[] GetReward()
    {
        return _rewards;
    }
    private void Update(TextImage buffer)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(buffer.GetString());
    }
}