using System.Text.Json;
public class CompoundEncounter : Encounter
{
    public string[] _text;
    public CompoundEncounter(JsonElement cardData, Item[] itemList) : base(cardData, itemList)
    {

    }
    public override Boolean Run(Player player, TextImage screen)
    {
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
}