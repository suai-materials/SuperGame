namespace SuperGame;

public class Game : IScreen
{
    private GameEngine _gameEngine;
    public GameItem SelectedItem;

    public GameItem[] GameItems = new[]
        {new BasicItem('X', ConsoleColor.Blue), new BasicItem('Y', ConsoleColor.Yellow), new BasicItem('@'), new BasicItem('@', ConsoleColor.Magenta)};

    public (int Width, int Height) size = (20, 3);
    private Random _random = new Random();
    public List<List<GameItem>> Board = new();
    int _cWidth, _cHeight;
    private (int x, int y) choosedItemPos = (0, 0);
    private bool _isFinish;
    private bool isChoosed = false;
    private int _side;
    private Task _choosedTask;


    public Game(GameEngine gameEngine)
    {
        Board = new List<List<GameItem>>();
        for (int i = 0; i < size.Width; i++)
        {
            Board.Add(new List<GameItem>());
            for (int j = 0; j < size.Height; j++)
            {
                Board[i].Add(GameItems[_random.Next(GameItems.Length)]);
            }
        }
        _gameEngine = gameEngine;
        Display();
        
    }
    


    public void DrawSelecetdItem(bool isSelected = false)
    {
        var item = Board[choosedItemPos.x][choosedItemPos.y];
        (int x, int y) pos = (_cWidth / 2 - _side * size.Width / 2 + _side * choosedItemPos.x, _cHeight / 2 - _side * size.Height / 2 + _side * choosedItemPos.y);
        Utils.WriteLinesOnPos(pos.x, pos.y,
            item.Display(_side), item.Color, isSelected ? ConsoleColor.DarkGray :Console.BackgroundColor);
    }

    public void DrawChoosedItem()
    {
        while (isChoosed)
        {
            DrawSelecetdItem();
            
            DrawSelecetdItem(true);
        }
    }

    public void CheckInput()
    {
        while (_gameEngine.GameStatus == GameStatus.Playing)
        {
            while (!Console.KeyAvailable)
            {
                if (_isFinish) return;
            }

            if (!isChoosed)
            {
                var key = Console.ReadKey().Key;
                if (Constants.UsedKeys.Contains(key))
                {
                    DrawSelecetdItem();
                }

                switch (key)
                {
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (choosedItemPos.y == size.Height - 1)
                        {
                            choosedItemPos.y = 0;
                        }
                        else
                        {
                            choosedItemPos.y += 1;
                        }
                        

                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:

                        if (choosedItemPos.y == 0)
                        {
                            choosedItemPos.y = size.Height - 1;
                        }
                        else
                        {
                            choosedItemPos.y -= 1;
                        }

                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (choosedItemPos.x == size.Width - 1)
                        {
                            choosedItemPos.x = 0;
                        }
                        else
                        {
                            choosedItemPos.x += 1;
                        }

                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (choosedItemPos.x == 0)
                        {
                            choosedItemPos.x = size.Width - 1;
                        }
                        else
                        {
                            choosedItemPos.x -= 1;
                        }

                        break;
                    case ConsoleKey.Enter: 
                    case ConsoleKey.Spacebar:
                        isChoosed = true;
                        // _choosedTask = Task.Run(() => DrawChoosedItem());
                        break;
                }
                DrawSelecetdItem(true);
            }
            else
                throw new NotImplementedException();

            //Display();
        }
    }


    public void Display()
    {
        Console.Clear();
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        _side = Math.Min(_cWidth, _cHeight) / Math.Min(size.Height, size.Width);
        while (_side * size.Width > _cWidth - 1 || _side * size.Height > _cHeight - 1)
        {
            _side--;
        }

        (int x, int y) pos = (_cWidth / 2 - _side * size.Width / 2, _cHeight / 2 - _side * size.Height / 2);


        for (int i = 0; i < size.Width; i++)
        {
            pos.y = _cHeight / 2 - _side * size.Height / 2;
            for (int j = 0; j < size.Height; j++)
            {
                if (i == choosedItemPos.x && j == choosedItemPos.y)
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(_side), Board[i][j].Color, ConsoleColor.DarkGray);
                }
                else
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(_side), Board[i][j].Color);
                }

                pos.y += _side;
            }

            pos.x += _side;
        }
    }

    public void Finish()
    {
        _isFinish = true;
    }

    public void OnReConfigure()
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        var _inputTask = Task.Run(() => CheckInput());
        _inputTask.Wait();
    }
}