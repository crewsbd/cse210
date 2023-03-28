using System.Text.Json;
public class BossEncounter : Encounter
{
    public BossEncounter(JsonElement cardData, Item[] itemsList) : base(cardData, itemsList)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
       public override bool Reject(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
        public override Item[] GetReward()
    {
        return _rewards;
    }
}