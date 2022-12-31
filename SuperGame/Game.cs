using System.Runtime.InteropServices.ComTypes;

namespace SuperGame;

public class Game : IScreen
{
    private GameEngine _gameEngine;

    public GameItem[] GameItems =
    {
        new BasicItem('X', ConsoleColor.Blue), new BasicItem('Y', ConsoleColor.Yellow), new BasicItem('@'),
        new BasicItem('@', ConsoleColor.Magenta), new BasicItem('&', ConsoleColor.Red)
    };

    private readonly (int Width, int Height) _size = (20, 3);
    private Random _random = new Random();
    public List<List<GameItem>> Board;
    int _cWidth, _cHeight;
    private (int x, int y) _choosedItemPos = (0, 0);
    private bool _isFinish;
    private bool _isChoosed = false;
    private GameStatus _gameStatus = GameStatus.NotChoose;
    private int _side;
    private Task _choosedTask;


    public Game(GameEngine gameEngine)
    {
        Board = new List<List<GameItem>>();
        for (int i = 0; i < _size.Width; i++)
        {
            Board.Add(new List<GameItem>());
            for (int j = 0; j < _size.Height; j++)
            {
                Board[i].Add(GameItems[_random.Next(GameItems.Length)]);
            }
        }

        _gameEngine = gameEngine;
        Display();
        Process();
    }


    public void DrawSelecetdItem(bool isSelected = false)
    {
        var item = Board[_choosedItemPos.x][_choosedItemPos.y];
        (int x, int y) pos = (_cWidth / 2 - _side * _size.Width / 2 + _side * _choosedItemPos.x,
            _cHeight / 2 - _side * _size.Height / 2 + _side * _choosedItemPos.y);
        Utils.WriteLinesOnPos(pos.x, pos.y,
            item.Display(_side), item.Color, isSelected ? ConsoleColor.DarkGray : Console.BackgroundColor);
    }

    public async Task DrawChoosedItem()
    {
        while (_gameStatus == GameStatus.Choosen)
        {
            DrawSelecetdItem();
            await Task.Delay(100);
            DrawSelecetdItem(true);
            await Task.Delay(100);
        }
    }

    public void CheckInput()
    {
        while (_gameEngine.GameEngineStatus == GameEngineStatus.Playing)
        {
            while (!Console.KeyAvailable)
            {
                if (_isFinish) return;
            }

            if (_gameStatus == GameStatus.NotChoose)
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
                        if (_choosedItemPos.y == _size.Height - 1)
                        {
                            _choosedItemPos.y = 0;
                        }
                        else
                        {
                            _choosedItemPos.y += 1;
                        }


                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:

                        if (_choosedItemPos.y == 0)
                        {
                            _choosedItemPos.y = _size.Height - 1;
                        }
                        else
                        {
                            _choosedItemPos.y -= 1;
                        }

                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (_choosedItemPos.x == _size.Width - 1)
                        {
                            _choosedItemPos.x = 0;
                        }
                        else
                        {
                            _choosedItemPos.x += 1;
                        }

                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (_choosedItemPos.x == 0)
                        {
                            _choosedItemPos.x = _size.Width - 1;
                        }
                        else
                        {
                            _choosedItemPos.x -= 1;
                        }

                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        _gameStatus = GameStatus.Choosen;
                        _choosedTask = DrawChoosedItem();

                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        string warning = "Нажмите Esc ещё раз, если хотите сдаться, если нет, нажмите любую кнопку";
                        Utils.WriteLinesOnPos(_cWidth / 2 - warning.Length / 2, _cHeight / 2, warning);
                        _gameStatus = GameStatus.WantEscape;
                        break;
                }

                if (_gameStatus != GameStatus.WantEscape)
                    DrawSelecetdItem(true);
            }
            else if (_gameStatus == GameStatus.Choosen)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    // Можно перевести игру (Game) в static
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        Board[_choosedItemPos.x][_choosedItemPos.y].Move(_choosedItemPos.x, _choosedItemPos.y, 0, 1, this);
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        Board[_choosedItemPos.x][_choosedItemPos.y].Move(_choosedItemPos.x, _choosedItemPos.y, 0, -1, this);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        Board[_choosedItemPos.x][_choosedItemPos.y].Move(_choosedItemPos.x, _choosedItemPos.y, 1, 0, this);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        Board[_choosedItemPos.x][_choosedItemPos.y].Move(_choosedItemPos.x, _choosedItemPos.y, -1, 0, this);
                        break;
                }
                
            }
            else if (_gameStatus == GameStatus.WantEscape)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        this.Finish();
                        break;
                    default:
                        _gameStatus = GameStatus.NotChoose;
                        Display();
                        break;
                }
            }

            //Display();
        }
    }

    public void CheckEmpty()
    {
        for (int i = _size.Width - 1; i >= 0; i--)
        {
            for (int j = _size.Height - 1; j >= 0; j--)
            {
                if (Board[i][j] is EmptyItem)
                {
                    Board[i][j] = GameItems[_random.Next(GameItems.Length)];
                }
            }
        }
    }

    public void Process()
    {
        _gameStatus = GameStatus.Processing;
        bool isNotChanged = false;
        for (int j = _size.Height - 1; j >= 0; j--)
        {
            for (int i = _size.Width - 1; i >= 0; i--)
            {
                (int h, int v) = (i, j);
                var item = Board[i][j];
                if (item is EmptyItem)
                {
                    continue;
                }

                int howH = 1;
                int howV = 1;
                h--;
                // По хорошему надо переопределить сравнение item.
                while (h >= 0 && Board[h][j].DisplayChar == item.DisplayChar && Board[h][j].Color == item.Color)
                {
                    howH++;
                    h--;
                }

                v--;
                while (v >= 0 && Board[i][v].DisplayChar == item.DisplayChar && Board[i][v].Color == item.Color)
                {
                    howV++;
                    v--;
                }

                if (howV >= 3)
                {
                    for (int hj = 0; hj < howV; hj++)
                    {
                        Board[i][j - hj] = new EmptyItem();
                    }

                    goto reprocess;
                }

                if (howH >= 3)
                {
                    for (int hi = 0; hi < howH; hi++)
                    {
                        Board[i - hi][j] = new EmptyItem();
                    }

                    goto reprocess;
                }
            }
        }

        _gameStatus = GameStatus.NotChoose;
        isNotChanged = true;
        reprocess:
        if (!isNotChanged)
        {
            Display();
            CheckEmpty();
            Thread.Sleep(500);
            Display();
            Thread.Sleep(500);
            Process();
        }
    }


    public void Display()
    {
        Console.Clear();
        
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        _side = Math.Min(_cWidth, _cHeight) / Math.Min(_size.Height, _size.Width);
        while (_side * _size.Width > _cWidth - 1 || _side * _size.Height > _cHeight - 1)
        {
            _side--;
        }

        (int x, int y) pos = (_cWidth / 2 - _side * _size.Width / 2, _cHeight / 2 - _side * _size.Height / 2);


        for (int i = 0; i < _size.Width; i++)
        {
            pos.y = _cHeight / 2 - _side * _size.Height / 2;
            for (int j = 0; j < _size.Height; j++)
            {
                if (i == _choosedItemPos.x && j == _choosedItemPos.y)
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
        _gameEngine.GameEngineStatus = GameEngineStatus.NotStarted;
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