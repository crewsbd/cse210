using System.Text.Json;
public class CompoundEncounter : Encounter
{
    private List<Encounter> _encounters;
    private int _encounterCount;
    public CompoundEncounter(JsonElement cardData, Item[] itemList, Table table) : base(cardData, itemList, table)
    {
        _encounterCount = cardData.GetProperty("SubCards").GetInt32();
        _encounters = new List<Encounter>();

        _card.DrawCard(0, 0, 35, 13);
        _card.Draw($"{_name}\n{Helpers.WrapText(_description, 33)}\n\n{_cardImage}\n\n\n\n\n\nReward...", 1, 1);
        _card.Draw(Helpers.WrapText($"You'll need to fight {_encounterCount} random monsters from the deck.",33), 1,10);
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