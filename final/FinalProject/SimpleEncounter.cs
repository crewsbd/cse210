using System.Text.Json;
class SimpleEncounter : Encounter
{
    public SimpleEncounter(JsonElement cardData) : base(cardData)
    {

    }
    public override Boolean Run(Player player, TextImage screen)
    {
        string originalBuffer = screen.GetString();
        screen.DrawCard(2, 2, 20, 20);
        screen.Draw($"You attack the {_name} ", 3, 3);
        Update(screen);
        Thread.Sleep(500);
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
    private void Update(TextImage buffer)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(buffer.GetString());
    }
}