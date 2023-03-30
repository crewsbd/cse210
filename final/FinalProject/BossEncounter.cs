using System.Text.Json;
public class BossEncounter : Encounter
{
    public BossEncounter(JsonElement cardData, Item[] itemsList, Table table) : base(cardData, itemsList, table)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        player.GiveItems(_rewards);
        return true;
    }
    public override bool Reject(Player player, TextImage screen)
    {
        return true;
    }
    public override Item[] GetReward() //Not used
    {
        return _rewards;
    }
}