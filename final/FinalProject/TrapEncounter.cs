using System.Text.Json;
class TrapEncounter : Encounter
{
    private Dictionary<string, int> _damageBonuses;
    public TrapEncounter(JsonElement cardData, Item[] itemsList, Table table) : base(cardData, itemsList, table)
    {
        _damageBonuses = new Dictionary<string, int>();
        JsonElement dmgB = cardData.GetProperty("DamageBonuses");
        _damageBonuses["Physical"] = dmgB.GetProperty("Physical").GetInt32();
        _damageBonuses["Life"] = dmgB.GetProperty("Life").GetInt32();
        _damageBonuses["Decay"] = dmgB.GetProperty("Decay").GetInt32();
        _damageBonuses["Fire"] = dmgB.GetProperty("Fire").GetInt32();
        _damageBonuses["Ice"] = dmgB.GetProperty("Ice").GetInt32();

        _card.DrawCard(0, 0, 35, 13);
        _card.Draw($"{_name}\n{Helpers.WrapText(_description, 33)}\n\n{_cardImage}\n\n\n\n\n\nReward...", 1, 1);
        _card.Draw($"DMG P:{_damageBonuses["Physical"]} L:{_damageBonuses["Life"]} D:{_damageBonuses["Decay"]} F:{_damageBonuses["Fire"]} I:{_damageBonuses["Ice"]}", 1, 10);
    }
    public override Boolean Run(Player player, TextImage screen)
    {
        string originalBuffer = screen.GetString();
        screen.DrawCard(2, 2, 40, 20);
        screen.Draw($"You brave the {_name} ⤷", 3, 3);
        Update(screen);
        Console.ReadKey();

        //ITEM SELECTION START---------------------------------------------------
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
            for (int c = 0; c < selectedCards.Count(); c++)
            {
                if (selectedCards[c]) //if this single use item was selected
                {
                    if (singleUseItems[c].HealthBonus > 0)
                    {
                        player.BoostHealth(singleUseItems[c].HealthBonus);  //Use potions
                    }
                    player.RemoveItem(singleUseItemOriginalIndex[c] - c); //Remove the used card from the player
                }
            }
        }
        //ITEM SELECTION END----------------------------------------------------

        //Get buffs
        Dictionary<string, int> dmgBonus = player.GetDamageBonuses();
        Dictionary<string, int> defBonus = player.GetDefenseBonuses();

        //Trap loop
        int eDMG = Math.Max(0, _damageBonuses["Physical"] - defBonus["Physical"]);
        eDMG += Math.Max(0, _damageBonuses["Life"] - defBonus["Life"]);
        eDMG += Math.Max(0, _damageBonuses["Decay"] - defBonus["Decay"]);
        eDMG += Math.Max(0, _damageBonuses["Fire"] - defBonus["Fire"]);
        eDMG += Math.Max(0, _damageBonuses["Ice"] - defBonus["Ice"]);
        eDMG = Math.Max(1, eDMG); //At least 1 damage
        player.InflictDamage(eDMG);
        Helpers.Notify($"The {_name} did {eDMG} to {player.Name()}", screen);

        player.RemoveHealthBoost(); //Remove any benefits of potions
        if (!player.IsDead()) //Player is victorious
        {
            player.GiveItems(_rewards);
            return true;
        }
        else //Player died
        {
            return false;
        }
    }
    public override Boolean Reject(Player player, TextImage screen) //Same as run, but damage is half
    {
        string originalBuffer = screen.GetString();
        screen.DrawCard(2, 2, 40, 20);
        screen.Draw($"You try to avoid the {_name}.⤷", 3, 3);
        Update(screen);
        Console.ReadKey();

        //ITEM SELECTION START---------------------------------------------------
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
            for (int c = 0; c < selectedCards.Count(); c++)
            {
                if (selectedCards[c]) //if this single use item was selected
                {
                    if (singleUseItems[c].HealthBonus > 0)
                    {
                        player.BoostHealth(singleUseItems[c].HealthBonus);  //Use potions
                    }
                    player.RemoveItem(singleUseItemOriginalIndex[c] - c); //Remove the used card from the player
                }
            }
        }
        //ITEM SELECTION END----------------------------------------------------

        //Get buffs
        Dictionary<string, int> dmgBonus = player.GetDamageBonuses();
        Dictionary<string, int> defBonus = player.GetDefenseBonuses();

        //Trap loop
        int eDMG = Math.Max(0, _damageBonuses["Physical"] - defBonus["Physical"]);
        eDMG += Math.Max(0, _damageBonuses["Life"] - defBonus["Life"]);
        eDMG += Math.Max(0, _damageBonuses["Decay"] - defBonus["Decay"]);
        eDMG += Math.Max(0, _damageBonuses["Fire"] - defBonus["Fire"]);
        eDMG += Math.Max(0, _damageBonuses["Ice"] - defBonus["Ice"]);
        eDMG = Math.Max(1, eDMG/2); //At least 1 damage
        player.InflictDamage(eDMG);
        Helpers.Notify($"The {_name} did {eDMG} to {player.Name()}", screen);

        player.RemoveHealthBoost(); //Remove any benefits of potions
        if (!player.IsDead()) //Player is victorious
        {
            return true;
        }
        else //Player died
        {
            return false;
        }
    }
    public override Item[] GetReward()
    {
        return _rewards;
    }
}