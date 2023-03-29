using System.Text.Json;
public class Item
{
    private string _name;
    private string _description;
    private string _image;
    private Boolean _singleUse;
    private int _healthBonus;
    private Dictionary<string, int> _damageBonuses;
    private Dictionary<string, int> _defenseBonuses;
    public Item(JsonElement cardData)
    {
        _name = cardData.GetProperty("Name").GetString();
        _description = cardData.GetProperty("Description").GetString();
        _singleUse = cardData.GetProperty("SingleUse").GetBoolean();
        _image = cardData.GetProperty("Image").GetString();
        _healthBonus = cardData.GetProperty("HealthBonus").GetInt32();
        _damageBonuses = new Dictionary<string, int>();
        _defenseBonuses = new Dictionary<string, int>();

        _damageBonuses["Physical"] = cardData.GetProperty("DamageBonuses").GetProperty("Physical").GetInt32();
        _damageBonuses["Life"] = cardData.GetProperty("DamageBonuses").GetProperty("Life").GetInt32();
        _damageBonuses["Decay"] = cardData.GetProperty("DamageBonuses").GetProperty("Decay").GetInt32();
        _damageBonuses["Fire"] = cardData.GetProperty("DamageBonuses").GetProperty("Fire").GetInt32();
        _damageBonuses["Ice"] = cardData.GetProperty("DamageBonuses").GetProperty("Ice").GetInt32();

        _defenseBonuses["Physical"] = cardData.GetProperty("DefenseBonuses").GetProperty("Physical").GetInt32();
        _defenseBonuses["Life"] = cardData.GetProperty("DefenseBonuses").GetProperty("Life").GetInt32();
        _defenseBonuses["Decay"] = cardData.GetProperty("DefenseBonuses").GetProperty("Decay").GetInt32();
        _defenseBonuses["Fire"] = cardData.GetProperty("DefenseBonuses").GetProperty("Fire").GetInt32();
        _defenseBonuses["Ice"] = cardData.GetProperty("DefenseBonuses").GetProperty("Ice").GetInt32();

    }
    public string Name
    {
        get { return _name; }
    }
    public Boolean SingleUse
    {
        get { return _singleUse; }
    }


}