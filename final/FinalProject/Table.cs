using System.Text.Json;
public class Table
{
    private int _width, _height;
    private int _currentPlayer;
    private int _cursorx, _cursory;
    private int _cardLandscapeX, _cardLandscapeY;
    private int _cardPortaitX, _cardPortraitY;
    private int _gap;
    private List<Player> _players;
    private Encounter[,] _encounters;
    private List<Encounter> _easyDeck;
    private List<Encounter> _mediumDeck;
    private List<Encounter> _hardDeck;
    private List<Encounter> _bossDeck;
    private List<Item> _items;
    private TextImage _tableImage;
    private string[] _promptList;
    private int _promptIndex;
    private enum GameState
    {
        StartTurn,
        FlippedEncounter,
        EncounterAccepted,
        EncounterRejected,
        EndTurn,
        EndGame

    }
    private GameState _gameState;

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
        _players = new List<Player>();
        _encounters = new Encounter[3, 3];
        _tableImage = new TextImage(_width, _height);
        _easyDeck = new List<Encounter>();
        _mediumDeck = new List<Encounter>();
        _hardDeck = new List<Encounter>();
        _bossDeck = new List<Encounter>();
        _items = new List<Item>();
        _gameState = GameState.StartTurn;
        _promptIndex = 1;
        _promptList = new string[] { "Default Prompt", "1", "2" };
        LoadDecks();
        ShuffleDecks();

        PlaceDungeon();

    }
    public void AddPlayer(string name)
    {
        _players.Add(new Player(name, 10));
        //_players[_players.Count()-1].GiveItems(new Item[] {new Item(), new Item(), new Item()}); //TESTING ONLY.  REMOVE THIS
    }
    public void NextPlayer() //I'm not going to use this
    {

    }
    public void StartGame()
    {
        Console.CursorVisible = false;
        Console.Clear();
        TrimDecks(); //Make decks appropriate for play count
        InsertBoss(); //Put a boss in hard deck
        do
        {
            DrawTable();
            Logic();
        } while (_gameState != GameState.EndGame);
        return;
    }
    private void Logic()
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
            case (ConsoleKey.LeftArrow):
                {
                    if (_gameState == GameState.StartTurn)
                    {
                        _cursorx -= 1;
                        if (_cursorx < 0)
                        {
                            _cursorx = 2;
                        }
                    }
                    break;
                }
            case (ConsoleKey.RightArrow):
                {
                    if (_gameState == GameState.StartTurn)
                    {
                        _cursorx += 1;
                        if (_cursorx > 2)
                        {
                            _cursorx = 0;
                        }
                    }
                    break;
                }
            case (ConsoleKey.DownArrow):
                {
                    if (_gameState == GameState.StartTurn)
                    {
                        _cursory += 1;
                        if (_cursory > 2)
                        {
                            _cursory = 0;
                        }
                    }
                    else if (_gameState == GameState.FlippedEncounter)
                    {
                        _promptIndex += 1;
                        if (_promptIndex > _promptList.Count() - 1)
                        {
                            _promptIndex = 1; //Beause first line is the prompt
                        }
                    }
                    break;
                }
            case (ConsoleKey.UpArrow):
                {
                    if (_gameState == GameState.StartTurn)
                    {
                        _cursory -= 1;
                        if (_cursory < 0)
                        {
                            _cursory = 2;
                        }
                    }
                    else if (_gameState == GameState.FlippedEncounter)
                    {
                        _promptIndex -= 1;
                        if (_promptIndex < 1) //Beause first line is the prompt
                        {
                            _promptIndex = _promptList.Count() - 1;
                        }
                    }
                    break;
                }
            case (ConsoleKey.Enter):
                {
                    if (_gameState == GameState.StartTurn)
                    {
                        _gameState = GameState.FlippedEncounter;
                        _promptList = new string[] { "Do you want to play this card?", "Accept", "Reject" };
                    }
                    else if (_gameState == GameState.FlippedEncounter) //Accepted or Rejected card
                    {
                        if (_promptIndex == 1) //accept
                        {
                            _gameState = GameState.EncounterAccepted;
                            if (_encounters[_cursorx, _cursory].Run(_players[_currentPlayer], _tableImage)) //success
                            {
                                //Put a new card out!!!!!!!!
                                _encounters[_cursorx, _cursory] = GetNextCard();
                            }
                            else //The encounter was a failure.
                            {
                                //No reward.  Damage done in encounter class? Should I follow this pattern for getting items?
                            }
                        }
                        else //reject
                        {
                            _gameState = GameState.EncounterRejected;
                            if (_encounters[_cursorx, _cursory].Reject(_players[_currentPlayer], _tableImage)) //Bravely ran away away.
                            {
                                //Can't see anything happening here.  Might end up changing
                            }
                            else //Bad thing happens.  Only traps do this.
                            {

                            }
                        }
                        if (_players[_currentPlayer].IsDead())
                        {
                            Helpers.Notify($"{_players[_currentPlayer].Name()} has died!", _tableImage);
                            _players.RemoveAt(_currentPlayer);
                            if (_players.Count() < 2)
                            {
                                Helpers.Notify($"{_players[0].Name()} is the winner!", _tableImage);
                                _gameState = GameState.EndGame;
                            }

                        }
                        if (_gameState != GameState.EndGame)
                        {
                            _currentPlayer = (_currentPlayer + 1) % _players.Count(); //next player
                            _gameState = GameState.StartTurn;
                        }
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
        for (int c = 0; c < _players[_currentPlayer].Items.Count(); c++)
        {
            _tableImage.DrawCard(c * (_cardPortaitX + _gap) + 1, 16, _cardPortaitX, _cardPortraitY);
            _tableImage.Draw(Helpers.WrapText(_players[_currentPlayer].Items[c].Name, _cardPortaitX - 2), c * (_cardPortaitX + _gap) + 2, 17);
        }

        //Draw current player
        _tableImage.Draw($"┨ Current Player: {_players[_currentPlayer].Name()} HP: {_players[_currentPlayer].Health}/{_players[_currentPlayer].MaxHealth}  ┠", 2, 0);
        //Draw currently selected card
        _tableImage.DrawHighlight(_cursorx * (_cardLandscapeX + _gap) + 1, _cursory * (_cardLandscapeY + _gap) + 1, _cardLandscapeX, _cardLandscapeY);
        _tableImage.Draw(_players[_currentPlayer].PlayerStatsDisplay(), 40, 3);

        //Draw a zoomed and flipped version of currently selected card(press enter to do so)
        if (_gameState == GameState.FlippedEncounter)
        {
            /*_tableImage.DrawCard(3, 2, 35, 13);
            _tableImage.Draw($"   Name\nDescription\n\nImage\n\n\n\n\n\nReward...", 5, 3);
            _tableImage.DrawCard(4, 6, 33, 6); */
            _tableImage.Draw(_encounters[_cursorx, _cursory].GetImage(), 3, 2);
            _tableImage.DrawCard(40, 2, 39, _promptList.Count() + 2);
            _tableImage.Draw(RenderPrompt(), 41, 3);
        }
        //Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(_tableImage.GetString());
    }
    private void LoadDecks()
    {
        string deckFile = File.ReadAllText("Resources/Cards.json");
        JsonElement deckData = JsonDocument.Parse(deckFile).RootElement;

        JsonElement items = deckData.GetProperty("Items"); //Start loading the items. One of each. Must happend before loading encounters
        for (int i = 0; i < items.GetArrayLength(); i++)
        {
            JsonElement currentCard = items[i];
            _items.Add(new Item(currentCard));
        }
        LoadDeck(ref _easyDeck, deckData.GetProperty("Encounters").GetProperty("Easy"));
        LoadDeck(ref _mediumDeck, deckData.GetProperty("Encounters").GetProperty("Medium"));
        LoadDeck(ref _hardDeck, deckData.GetProperty("Encounters").GetProperty("Hard"));
        LoadDeck(ref _bossDeck, deckData.GetProperty("Encounters").GetProperty("Boss"));
    }

    private void ShuffleDecks()
    {
        ShuffleDeck(ref _easyDeck);
        ShuffleDeck(ref _mediumDeck);
        ShuffleDeck(ref _hardDeck);
        ShuffleDeck(ref _bossDeck);
    }
    private void TrimDecks()
    {
        _easyDeck = _easyDeck.GetRange(0, _players.Count * 3);
        _mediumDeck = _mediumDeck.GetRange(0, _players.Count * 3);
        _hardDeck = _hardDeck.GetRange(0, _players.Count * 3);
    }
    private void InsertBoss()
    {
        _hardDeck.Insert(new Random(DateTime.Now.Millisecond).Next(_players.Count() * 3), _bossDeck[0]);
    }
    private void LoadDeck(ref List<Encounter> deck, JsonElement json)
    {
        for (int i = 0; i < json.GetArrayLength(); i++)
        {
            JsonElement currentCard = json[i];
            string type = currentCard.GetProperty("Type").GetString();
            int inDeck = currentCard.GetProperty("InDeck").GetInt32();

            if (type == "SimpleEncounter")
            {   //Load a simple card set
                for (int num = 0; num < inDeck; num++)
                {
                    deck.Add(new SimpleEncounter(currentCard, _items.ToArray(), this));
                }
            }
            else if (type == "CompoundEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    deck.Add(new CompoundEncounter(currentCard, _items.ToArray(), this));
                }
            }
            else if (type == "BossEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    deck.Add(new BossEncounter(currentCard, _items.ToArray(), this));
                }
            }
            else if (type == "TrapEncounter")
            {
                for (int num = 0; num < inDeck; num++)
                {
                    deck.Add(new TrapEncounter(currentCard, _items.ToArray(), this));
                }
            }
        }
    }
    public void PlaceDungeon()
    {
        //Deal to the dungeon grid
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _encounters[x, y] = GetNextCard();
            }
        }
    }
    private void ShuffleDeck(ref List<Encounter> deck)
    {
        Random rnd = new Random(DateTime.Today.Millisecond);
        deck = deck.OrderBy((card) => rnd.Next()
        ).ToList();
    }
    public Encounter GetNextCard()
    {
        Encounter nextCard;
        if (_easyDeck.Count > 0)
        {
            nextCard = Helpers.ListPop(_easyDeck);
        }
        else if (_mediumDeck.Count > 0)
        {
            nextCard = Helpers.ListPop(_mediumDeck);
        }
        else if (_hardDeck.Count > 0)
        {
            nextCard = Helpers.ListPop(_hardDeck);
        }
        else
        {
            //What do I do if no cards left?
            string generoString = @"{
                ""Type"": ""SimpleEncounter"",
                ""Name"": ""Genero"",
                ""Description"": ""Who's this guy?"",
                ""Image"": ""Monster"",
                ""InDeck"": 6,
                ""Attack"": 1,
                ""AttackType"": ""Physical"",
                ""Health"": 1,
                ""Weakness"": ""None"",
                ""WeaknessType"": ""None"",
                ""Rewards"": [""Small Health Potion""]
            }";
            JsonElement genero = JsonDocument.Parse(generoString).RootElement;

            nextCard = new SimpleEncounter(genero, _items.ToArray(), this);
        }
        return nextCard;
    }

    private string RenderPrompt()
    {
        string returnString = _promptList[0];
        for (int line = 1; line < _promptList.Count(); line++)
        {
            returnString += $"\n{(line == _promptIndex ? "->" : "  ")} {_promptList[line]}";
        }
        return returnString;
    }

}