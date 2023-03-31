using System.Text.Json;

public class Player
{
    private string _name;
    private List<Item> _items;
    private Reservoir _health;
    private Boolean _isDead;
    public Player(string name, int health)
    {
        _name = name;
        _health = new Reservoir(10, 10);
        _items = new List<Item>();
        //Give the player a starter weapon
        string swordString = @"{ 
            ""Name"": ""Heroes Sword"",
            ""Description"": ""Just a basic sword."",
            ""SingleUse"": false,
            ""Image"": ""Weapon"",
            ""HealthBonus"": 0,
            ""MaxCarried"": 1,
            ""DamageBonuses"": {
                ""Physical"": 2,
                ""Life"": 0,
                ""Decay"": 0,
                ""Fire"": 0,
                ""Ice"": 0
            },
            ""DefenseBonuses"": {
                ""Physical"": 0,
                ""Life"": 0,
                ""Decay"": 0,
                ""Fire"": 0,
                ""Ice"": 0
            }
        }";
        JsonElement sword = JsonDocument.Parse(swordString).RootElement;

        _items.Add(new Item(sword));
    }
    public Item[] Items
    {
        get { return _items.ToArray(); }
    }
    public void InflictDamage(int amount)
    {
        _health.Subtract(amount);
        if (_health.Value <= 0)
        {
            _isDead = true;
        }
    }
    public void Heal(int amount)
    {
        _health.Add(amount);
    }
    public void RemoveItem(int index)
    {
        _items.RemoveAt(index);
    }
    public void GiveItems(Item[] newItems)
    {
        foreach (Item item in newItems)
        {
            if (!IsItemMaxed(item))
            {
                _items.Add(item);
            }
        }
    }
    public string Name()
    {
        return _name;
    }
    public int Health
    {
        get { return _health.Value; }
    }
    public int MaxHealth
    {
        get { return _health.Max; }
    }
    public bool IsDead()
    {
        return _isDead;
    }
    private Boolean IsItemMaxed(Item item)
    {
        int itemCount = 0;
        for (int i = 0; i < _items.Count(); i++)
        {
            if (_items[i].Name == item.Name)
            {
                itemCount++;
            }
        }
        if (itemCount >= item.MaxCarried)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string PlayerStatsDisplay()
    {
        Dictionary<string, int> damageB = GetDamageBonuses();

        Dictionary<string, int> defenseB = GetDefenseBonuses();

        TextImage img = new TextImage(20, 10);
        img.Draw(_name, 1, 1);
        img.Draw($"HP: {_health.Value.ToString()}/{_health.Value.ToString()}", 1, 2);
        img.Draw("      DMG DEF", 1, 2);
        img.Draw($"Phy:  {damageB["Physical"]}", 1, 3);
        img.Draw($"Lif:  {damageB["Life"]}", 1, 4);
        img.Draw($"Dec:  {damageB["Decay"]}", 1, 5);
        img.Draw($"Fir:  {damageB["Fire"]}", 1, 6);
        img.Draw($"Ice:  {damageB["Ice"]}", 1, 7);

        img.Draw($"{defenseB["Physical"]}", 11, 3);
        img.Draw($"{defenseB["Life"]}", 11, 4);
        img.Draw($"{defenseB["Decay"]}", 11, 5);
        img.Draw($"{defenseB["Fire"]}", 11, 6);
        img.Draw($"{defenseB["Ice"]}", 11, 7);
        return img.GetString();
    }
    public Dictionary<string, int> GetDamageBonuses()
    {
        Dictionary<string, int> bonuses = new Dictionary<string, int>();
        bonuses["Physical"] = 0;
        bonuses["Life"] = 0;
        bonuses["Decay"] = 0;
        bonuses["Fire"] = 0;
        bonuses["Ice"] = 0;
        foreach (Item item in _items)
        {
            Dictionary<string, int> itemBonuses = item.GetDamageBonuses();
            bonuses["Physical"] += itemBonuses["Physical"];
            bonuses["Life"] += itemBonuses["Life"];
            bonuses["Decay"] += itemBonuses["Decay"];
            bonuses["Fire"] += itemBonuses["Fire"];
            bonuses["Ice"] += itemBonuses["Ice"];
        }
        return bonuses;
    }
    public Dictionary<string, int> GetDefenseBonuses()
    {
        Dictionary<string, int> bonuses = new Dictionary<string, int>();
        bonuses["Physical"] = 0;
        bonuses["Life"] = 0;
        bonuses["Decay"] = 0;
        bonuses["Fire"] = 0;
        bonuses["Ice"] = 0;
        foreach (Item item in _items)
        {
            Dictionary<string, int> itemBonuses = item.GetDefenseBonuses();
            bonuses["Physical"] += itemBonuses["Physical"];
            bonuses["Life"] += itemBonuses["Life"];
            bonuses["Decay"] += itemBonuses["Decay"];
            bonuses["Fire"] += itemBonuses["Fire"];
            bonuses["Ice"] += itemBonuses["Ice"];
        }
        return bonuses;
    }

}