using System.Text.Json;
class SimpleEncounter : Encounter
{
    public SimpleEncounter(JsonElement cardData) : base(cardData)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
}