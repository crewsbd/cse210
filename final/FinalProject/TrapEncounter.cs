using System.Text.Json;
class TrapEncounter : Encounter
{
    public TrapEncounter(JsonElement cardData, Item[] itemsList) : base(cardData, itemsList)
    {

    }
    public override Boolean Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
    public override Boolean Reject(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
        public override Item[] GetReward()
    {
        return _rewards;
    }
}