using System.Text.Json;
class Table
{
    private int _width, _height;
    private int _currentPlayer;
    private int _cursorx, _cursory;
    private int _cardLandscapeX, _cardLandscapeY;
    private int _cardPortaitX, _cardPortraitY;
    private int _gap;
    private Boolean _showingCard;
    private List<Player> _players;
    private Encounter[,] _encounters;
    private List<Encounter> _easyDeck;
    private List<Encounter> _mediumDeck;
    private List<Encounter> _hardDeck;
    private List<Encounter> _bossDeck;
    private List<Item> _itemDeck;
    private TextImage _tableImage;

    public Table(int width, int height)
    {
        _width = width;
        _height = height;
        _currentPlayer = 0;
        _cursorx = 0;
        _cursory = 0;
        _cardLandscapeX = 13;
        _cardLandscapeY = 5;
        _cardPortaitX = 9;
        _cardPortraitY = 7;
        _gap = 0;
        _showingCard = false;
        _players = new List<Player>();
        _encounters = new Encounter[3, 3];
        _tableImage = new TextImage(_width, _height);
        _easyDeck = new List<Encounter>();
        _mediumDeck = new List<Encounter>();
        _hardDeck = new List<Encounter>();
        _bossDeck = new List<Encounter>();
        _itemDeck = new List<Item>();
        LoadDecks();

    }
    public void AddPlayer(string name)
    {
        _players.Add(new Player(name, 10));
    }
    public void StartGame()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Boolean gameOver = false;
        do
        {
            DrawTable();
            HandleInput();
        } while (!gameOver);
    }
    private void HandleInput()
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
            case (ConsoleKey.LeftArrow):
                {
                    _cursorx -= 1;
                    if (_cursorx < 0)
                    {
                        _cursorx = 2;
                    }
                    break;
                }
            case (ConsoleKey.RightArrow):
                {
                    _cursorx += 1;
                    if (_cursorx > 2)
                    {
                        _cursorx = 0;
                    }
                    break;
                }
            case (ConsoleKey.DownArrow):
                {
                    _cursory += 1;
                    if (_cursory > 2)
                    {
                        _cursory = 0;
                    }
                    break;
                }
            case (ConsoleKey.UpArrow):
                {
                    _cursory -= 1;
                    if (_cursory < 0)
                    {
                        _cursory = 2;
                    }
                    break;
                }
            case (ConsoleKey.Enter):
                {
                    if (_showingCard)
                    {
                        _showingCard = false;
                    }
                    else
                    {
                        _showingCard = true;
                    }
                    break;
                }
        }
    }
    private void DrawTable()
    {
        _tableImage.DrawCard(0, 0, _width, _height);
        //Draw dungeon
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _tableImage.DrawCard(x * (_cardLandscapeX + _gap) + 1, y * (_cardLandscapeY + _gap) + 1, _cardLandscapeX, _cardLandscapeY);
                _tableImage.Draw("DUNGEON", x * (_cardLandscapeX + _gap) + 4, y * (_cardLandscapeY + _gap) + 2);
                _tableImage.Draw("ENCOUNTER", x * (_cardLandscapeX + _gap) + 3, y * (_cardLandscapeY + _gap) + 3);
            }
        }
        //Draw hand
        for (int x = 0; x < 6; x++)
        {
            _tableImage.DrawCard(x * (_cardPortaitX + _gap) + 1, 16, _cardPortaitX, _cardPortraitY);
        }

        //Draw current player
        _tableImage.Draw($"┨ Current Player: {_players[_currentPlayer].Name()} ┠", 2, 0);
        //Draw currently selected card
        _tableImage.DrawHighlight(_cursorx * (_cardLandscapeX + _gap) + 1, _cursory * (_cardLandscapeY + _gap) + 1, _cardLandscapeX, _cardLandscapeY);

        //Draw a zoomed and flipped version of currently selected card(press enter to do so)
        if (_showingCard)
        {
            _tableImage.DrawCard(3, 2, 35, 13);
            _tableImage.Draw("Name\nDescription\n\nImage\n\n\n\n\n\nReward...", 5,3);
            _tableImage.DrawCard(4, 6, 33,6);
        }
        //Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(_tableImage.GetString());
    }
    private void LoadDecks()
    {
        string deckFile = File.ReadAllText("Resources/Cards.json");
        JsonElement deckData = JsonDocument.Parse(deckFile).RootElement;

        JsonElement easyCards = deckData.GetProperty("Encounters").GetProperty("Easy");
        for (int i = 0; i < easyCards.GetArrayLength(); i++)
        {
            JsonElement currentCard = easyCards[i];
            string type = currentCard.GetProperty("Type").GetString();
            int inDeck = currentCard.GetProperty("InDeck").GetInt32();

            if (type == "SimpleEncounter")
            {   //Load a simple card set
                for (int num = 0; num < inDeck; num++)
                {
                    _easyDeck.Add(new SimpleEncounter(currentCard));
                }
            }
            else if (type == "CompoundEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    _easyDeck.Add(new CompoundEncounter(currentCard));
                }
            }
            else if (type == "BossEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    _easyDeck.Add(new BossEncounter(currentCard));
                }
            }
            else if (type == "TrapEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    _easyDeck.Add(new TrapEncounter(currentCard));
                }
            }
        }

    }
}