using System.Text.Json;
class SimpleEncounter : Encounter
{
    private int _health;
    private Dictionary<string, int> _damageBonuses;
    private Dictionary<string, int> _defenseBonuses;
    public SimpleEncounter(JsonElement cardData, Item[] items, Table table) : base(cardData, items, table)
    {
        _health = cardData.GetProperty("Health").GetInt32();
        _damageBonuses = new Dictionary<string, int>();
        _defenseBonuses = new Dictionary<string, int>();
        JsonElement dmgB = cardData.GetProperty("DamageBonuses");
        JsonElement defB = cardData.GetProperty("DefenseBonuses");
        _damageBonuses["Physical"] = dmgB.GetProperty("Physical").GetInt32();
        _damageBonuses["Life"] = dmgB.GetProperty("Life").GetInt32();
        _damageBonuses["Decay"] = dmgB.GetProperty("Decay").GetInt32();
        _damageBonuses["Fire"] = dmgB.GetProperty("Fire").GetInt32();
        _damageBonuses["Ice"] = dmgB.GetProperty("Ice").GetInt32();

        _defenseBonuses["Physical"] = defB.GetProperty("Physical").GetInt32();
        _defenseBonuses["Life"] = defB.GetProperty("Life").GetInt32();
        _defenseBonuses["Decay"] = defB.GetProperty("Decay").GetInt32();
        _defenseBonuses["Fire"] = defB.GetProperty("Fire").GetInt32();
        _defenseBonuses["Ice"] = defB.GetProperty("Ice").GetInt32();

        _card.DrawCard(0, 0, 35, 13);
        _card.Draw($"{_name}\n{Helpers.WrapText(_description, 33)}\n\n{_cardImage}\n\n\n\n\n\nReward...", 1, 1);
        _card.Draw($"DMG P:{_damageBonuses["Physical"]} L:{_damageBonuses["Life"]} D:{_damageBonuses["Decay"]} F:{_damageBonuses["Fire"]} I:{_damageBonuses["Ice"]}", 1,10);
        _card.Draw($"DEF P:{_defenseBonuses["Physical"]} L:{_defenseBonuses["Life"]} D:{_defenseBonuses["Decay"]} F:{_defenseBonuses["Fire"]} I:{_defenseBonuses["Ice"]}", 1,11);
    }
    public override Boolean Run(Player player, TextImage screen)
    {
        string originalBuffer = screen.GetString();
        screen.DrawCard(2, 2, 40, 20);
        screen.Draw($"You attack the {_name} ⤷", 3, 3);
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
                    if( singleUseItems[c].HealthBonus > 0 )
                    {
                        player.BoostHealth(singleUseItems[c].HealthBonus);  //Use potions
                    }
                    player.RemoveItem(singleUseItemOriginalIndex[c] - c); //Remove the used card from the player
                }
            }
        }
        //ITEM SELECTION END----------------------------------------------------

        /*Run card 
        1. Get all buffs from cards 
        2. Loop between player and enemy.
        2. a If not, player damaged. 
        2. b Use potions
        //3. See if player survives
        */

        //Get buffs
        Dictionary<string, int> dmgBonus = player.GetDamageBonuses();
        Dictionary<string, int> defBonus = player.GetDefenseBonuses();

        //Fight loop
        Boolean playerTurn = true;
        Boolean continueFight = true;
        int eHealth = _health;

        do
        {
            if (playerTurn)
            {
                int pDMG = dmgBonus["Physical"] - _defenseBonuses["Physical"];
                pDMG += dmgBonus["Life"] - _defenseBonuses["Life"];
                pDMG += dmgBonus["Decay"] - _defenseBonuses["Decay"];
                pDMG += dmgBonus["Fire"] - _defenseBonuses["Fire"];
                pDMG += dmgBonus["Ice"] - _defenseBonuses["Ice"];
                eHealth -= pDMG;
                Helpers.Notify($"{player.Name()} did {pDMG} to the {_name}", screen);
                if (eHealth <= 0)
                {
                    continueFight = false;
                }
            }
            else //Encounter turn
            {
                int eDMG = _damageBonuses["Physical"] - defBonus["Physical"];
                eDMG += _damageBonuses["Life"] - defBonus["Life"];
                eDMG += _damageBonuses["Decay"] - defBonus["Decay"];
                eDMG += _damageBonuses["Fire"] - defBonus["Fire"];
                eDMG += _damageBonuses["Ice"] - defBonus["Ice"];
                player.InflictDamage(eDMG);
                Helpers.Notify($"The {_name} did {eDMG} to {player.Name()}", screen);
                if (player.IsDead())
                {
                    continueFight = false;
                }
            }
            playerTurn = !playerTurn;
        } while (continueFight);
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
    public override Boolean Reject(Player player, TextImage screen)
    {
        return true;
    }
    public override Item[] GetReward() //Not going to use this
    {
        return _rewards;
    }
    private void Update(TextImage buffer)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(buffer.GetString());
    }
}