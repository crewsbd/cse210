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

        //Select only single use items
        List<Item> singleUseItems = new List<Item>();
        List<int> singleUseItemOriginalIndex = new List<int>();
        for (int itemIndex = 0; itemIndex < player.Items.Count(); itemIndex++)
        {
            if (player.Items[itemIndex].SingleUse)
            {
                singleUseItems.Add(player.Items[itemIndex]);
                singleUseItemOriginalIndex.Add(itemIndex); //Got to be a better way
            }
        }


        if (singleUseItems.Count() > 0) //Begin item select loop
        {
            screen.Draw("Pick the cards you will use.", 3, 4);  //Need to filter this list to only one-use cards
            Update(screen);
            int focusedCard = 0; //Which card we looking at?
            Boolean[] selectedCards = new Boolean[singleUseItems.Count()]; //track selected card
            Array.Fill(selectedCards, false);
            Boolean selectingCards = true; //keep looping?
            string backgroundBackup = screen.GetString();
            do
            {
                screen.Draw(backgroundBackup, 0, 0);
                for (int c = 0; c < singleUseItems.Count() + 1; c++)  //Draw all the available cards
                {
                    if (c < singleUseItems.Count())
                    {
                        screen.DrawCard(c * 9 + 1, 16, 9, 7);
                        screen.Draw(Helpers.WrapText(singleUseItems[c].Name, 9), c * 9 + 2, 17);
                        if (selectedCards[c])
                        {
                            screen.Draw("-X-", c * 9 + 2, 17);
                        }
                    }
                    else //draw Confirm selection
                    {
                        screen.DrawCard(c * 9 + 1, 18, 9, 4);
                        screen.Draw("Confirm\nSelection", c * 9 + 2, 18);
                    }
                }

                //Draw focused card
                if (focusedCard < singleUseItems.Count())
                {
                    screen.DrawCard(focusedCard * 9, 15, 11, 9);
                    screen.Draw(Helpers.WrapText(singleUseItems[focusedCard].Name, 11), focusedCard * 9 + 1, 17);
                    if (selectedCards[focusedCard])
                    {
                        screen.Draw("-X-", focusedCard * 9 + 1, 16);
                    }
                }
                else //Or confirm selection
                {
                    screen.DrawCard(focusedCard * 9, 17, 11, 6);
                    screen.Draw("Confirm\nSelection", focusedCard * 9 + 1, 17);
                }

                Update(screen);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    focusedCard--;
                    if (focusedCard < 0)
                    {
                        focusedCard = singleUseItems.Count();
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    focusedCard = (focusedCard + 1) % (singleUseItems.Count() + 1);
                }
                else if (key == ConsoleKey.Enter)
                {
                    //Toggle the card
                    if (focusedCard < singleUseItems.Count())
                    {
                        selectedCards[focusedCard] = !selectedCards[focusedCard];
                    }
                    else
                    {
                        selectingCards = false;
                    }
                }
            } while (selectingCards);
            for(int c = 0; c < selectedCards.Count(); c++)
            {
                if(selectedCards[c]) //if this single use item was selected
                {
                    player.RemoveItem(singleUseItemOriginalIndex[c]-c); //Remove the used card from the player
                }
            }
        }

        /*Run card 
        1. Get all buffs from cards 
        2. Check to see if enemy defeated in one shot
        2. a If not, player damaged. 
        2. b Use potions
        //3. See if player survives
        */


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