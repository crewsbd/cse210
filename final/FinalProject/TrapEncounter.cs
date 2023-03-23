using System.Text.Json;
class TrapEncounter : Encounter
{
    public TrapEncounter(JsonElement cardData) : base(cardData)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
}