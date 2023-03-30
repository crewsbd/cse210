using System.Text.Json;
public class CompoundEncounter : Encounter
{
    private List<Encounter> _encounters;
    private int _encounterCount;
    public CompoundEncounter(JsonElement cardData, Item[] itemList, Table table) : base(cardData, itemList, table)
    {
        _encounterCount = cardData.GetProperty("SubCards").GetInt32();
        _encounters = new List<Encounter>();
    }
    public override Boolean Run(Player player, TextImage screen)
    {
        if (_encounters.Count() == 0)  //If encounter hasn't been run yet
        {
            LoadEncounters();
        }
        _encounters.ForEach(enc =>
        {
            enc.Run(player, screen);
        });

        player.GiveItems(_rewards);
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
    private void LoadEncounters()
    {
        for (int i = 0; i < _encounterCount; i++)
        {
            _encounters.Add(_table.GetNextCard());
        }
    }
}