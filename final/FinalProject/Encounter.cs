using System.Text.Json;
public abstract class Encounter
{
    protected string _name;
    private string _description;
    protected Item[] _rewards;
    private string _cardImage;
    private TextImage _card;
    public Encounter(JsonElement cardData, Item[] itemsList)
    {
        _name = cardData.GetProperty("Name").GetString();
        _description = cardData.GetProperty("Description").GetString();
        _cardImage = File.ReadAllText("Resources/" + cardData.GetProperty("Image").GetString() + ".txt");
        _card = new TextImage(35, 13);


    //This loop is getting all rewards.
        JsonElement rewards = cardData.GetProperty("Rewards"); //String list of all rewards for this card.
        List<Item> tempItems = new List<Item>(); //Make a temp list, turn it to an array at the end.
        for(int i = 0; i < rewards.GetArrayLength(); i++)  //For every reward name string...each reward this card should have
        {
            string rewardName = rewards[i].GetString(); //This is the name of the reward we need to find.
            for(int itemIndex = 0; itemIndex < itemsList.Count(); itemIndex++) //Search through the list of all items.
            {
                if(itemsList[itemIndex].Name == rewardName) //If we found the item...
                {
                    tempItems.Add(itemsList[itemIndex]); //Add the matching item to the temp list
                }
            }
        }
        _rewards = tempItems.ToArray(); //Put the final list


        _card.DrawCard(0, 0, 35, 13);
        _card.Draw($"{_name}\n{Helpers.WrapText(_description, 30)}\n\n{_cardImage}\n\n\n\n\n\nReward...", 1, 1);
        _card.DrawCard(1, 4, 33, 6);


        /* _card.Draw(_name, 2,1);
        _card.Draw(_description,2,3);
        _card.Draw(_cardImage, 5,2); */
    }
    public TextImage GetImage()
    {
        return _card;
    }
    abstract public Boolean Run(Player player, TextImage screen);
    abstract public Boolean Reject(Player player, TextImage screen);
    abstract public Item[] GetReward();
}