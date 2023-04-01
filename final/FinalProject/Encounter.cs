using System.Text.Json;
public abstract class Encounter
{
    protected string _name;
    protected string _description;
    protected Item[] _rewards;
    protected string _cardImage;
    protected TextImage _card;
    protected Table _table;

    public Encounter(JsonElement cardData, Item[] itemsList, Table table)
    {
        _name = cardData.GetProperty("Name").GetString();
        _description = cardData.GetProperty("Description").GetString();

        _cardImage = File.ReadAllText("Resources/" + cardData.GetProperty("Image").GetString() + ".txt");
        _card = new TextImage(35, 13);
        _table = table;

        //This loop is getting all rewards.
        JsonElement rewards = cardData.GetProperty("Rewards"); //String list of all rewards for this card.
        List<Item> tempItems = new List<Item>(); //Make a temp list, turn it to an array at the end.
        for (int i = 0; i < rewards.GetArrayLength(); i++)  //For every reward name string...each reward this card should have
        {
            string rewardName = rewards[i].GetString(); //This is the name of the reward we need to find.
            for (int itemIndex = 0; itemIndex < itemsList.Count(); itemIndex++) //Search through the list of all items.
            {
                if (itemsList[itemIndex].Name == rewardName) //If we found the item...
                {
                    tempItems.Add(itemsList[itemIndex]); //Add the matching item to the temp list
                }
            }
        }
        _rewards = tempItems.ToArray(); //Put the final list


        //_card.DrawCard(1, 4, 33, 6);
    }
    public TextImage GetImage()
    {
        return _card;
    }
    abstract public Boolean Run(Player player, TextImage screen);
    abstract public Boolean Reject(Player player, TextImage screen);
    abstract public Item[] GetReward();
    protected void Update(TextImage buffer)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(buffer.GetString());
    }
}