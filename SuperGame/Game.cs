namespace SuperGame;

public class Game : IScreen
{
    private GameEngine _gameEngine;
    public GameItem SelectedItem;

    public GameItem[] GameItems = new[]
        {new BasicItem('X', ConsoleColor.Blue), new BasicItem('Y', ConsoleColor.Yellow), new BasicItem('@')};

    public (int Width, int Height) size = (20, 3);
    private Random _random = new Random();
    public List<List<GameItem>> Board = new();
    int _cWidth, _cHeight;
    private (int x, int y) choosedItemPos = (0, 0);
    private bool _isFinish;
    private bool isChoosed = false;
    


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
        var _inputTask = Task.Run(() => CheckInput());
        _inputTask.Wait();
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
                switch (Console.ReadKey().Key)
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
                }
            else
                throw new NotImplementedException();
            Display();
        }
    }


    public void Display()
    {
        Console.Clear();
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        int side = Math.Min(_cWidth, _cHeight) / Math.Min(size.Height, size.Width);
        while (side * size.Width > _cWidth - 1 || side * size.Height > _cHeight - 1)
        {
            side--;
        }

        (int x, int y) pos = (_cWidth / 2 - side * size.Width / 2, _cHeight / 2 - side * size.Height / 2);
        var a = Console.CapsLock;
        for (int i = 0; i < size.Width; i++)
        {
            pos.y = _cHeight / 2 - side * size.Height / 2;
            for (int j = 0; j < size.Height; j++)
            {
                if (i == choosedItemPos.x && j == choosedItemPos.y)
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(side), Board[i][j].Color, ConsoleColor.White);
                }
                else
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(side), Board[i][j].Color);
                }

                pos.y += side;
            }

            pos.x += side;
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
}