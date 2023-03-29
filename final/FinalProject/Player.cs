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
}