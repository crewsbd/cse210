using System.Text.Json;
public class BossEncounter : Encounter
{
    public BossEncounter(JsonElement cardData) : base(cardData)
    {

    }
    public override bool Run(Player player, TextImage screen)
    {
        throw new NotImplementedException();
    }
}