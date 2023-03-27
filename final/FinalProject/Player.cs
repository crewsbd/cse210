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
    public void InflictDamage(int amount)
    {
        _health.Subtract(amount);
        if(_health.GetCurrent() <= 0)
        {
            _isDead = true;
        }
    }
    public void Heal(int amount)
    {
        _health.Add(amount);
    }
    public void GiveItems(Item[] newItems)
    {
        foreach(Item item in newItems)
        {
            _items.Add(item);
        }
    }
    public string Name()
    {
        return _name;
    }
    public bool IsDead()
    {
        return _isDead;
    }
}