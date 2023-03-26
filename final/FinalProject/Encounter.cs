using System.Text.Json;
public abstract class Encounter
{
    private string _name;
    private string _description;
    private Item[] _rewards;
    private string _cardImage;
    private TextImage _card;
    public Encounter(JsonElement cardData)
    {
        _name = cardData.GetProperty("Name").GetString();
        _description = cardData.GetProperty("Description").GetString();
        _cardImage = File.ReadAllText("Resources/" + cardData.GetProperty("Image").GetString() + ".txt" );
        _card = new TextImage(35, 13);

        _card.DrawCard(0, 0, 35, 13);
            _card.Draw($"{_name}\n{_description}\n\n{_cardImage}\n\n\n\n\n\nReward...", 1, 1);
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
}