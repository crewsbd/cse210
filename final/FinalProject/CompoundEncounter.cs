using System.Text.Json;
public class CompoundEncounter : Encounter
{
    public string[] _text;
    public CompoundEncounter(JsonElement cardData) : base(cardData)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
}