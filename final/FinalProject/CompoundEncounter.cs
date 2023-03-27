using System.Text.Json;
public class CompoundEncounter : Encounter
{
    public string[] _text;
    public CompoundEncounter(JsonElement cardData) : base(cardData)
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
        return new Item[] {new Item()};
    }
}