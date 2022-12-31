using System.Diagnostics;

namespace SuperGame;

public class GameEngine
{
    private GameEngineStatus _gameEngineStatus = GameEngineStatus.NotStarted;
    private Menu _menu;
    private Game _game;

    private Task _consoleSizeTask;

    public GameEngineStatus GameEngineStatus
    {
        get => _gameEngineStatus;
        set
        {
            _gameEngineStatus = value;
            switch (value)
            {
                case GameEngineStatus.Quit:
                    // Сохраняем что-то
                    _menu.Finish();
                    break;
                case GameEngineStatus.Playing:
                    _menu.Finish();
                    // consoleSizeTask = Task.Run(() => CheckConsoleSize());
                    _game = new Game(this);
                    _game.Start();
                    break;
                case GameEngineStatus.NotStarted:
                    _menu = new Menu(this);
                    _menu.Start();
                    break;
            }
        }
    }

    private byte _moves = Constants.MAX_MOVES;

    public byte Moves
    {
        get => _moves;
        set
        {
            if (value <= 0)
            {
                _moves = value;
                GameEngineStatus = GameEngineStatus.End;
            }
            else if (value > Constants.MAX_MOVES)
                _moves = Constants.MAX_MOVES;

            _moves = value;
        }
    }

    public GameEngine()
    {
        Utils.defaultBgColor = ConsoleColor.Black;
        Utils.defaultFgColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        _consoleSizeTask = Task.Run(() => CheckConsoleSize());
        _menu = new Menu(this);
        _menu.Start();
    }

    public void Move()
    {
    }

    private void CheckConsoleSize()
    {
        (int cWidth, int cHeight) = (Console.WindowWidth, Console.WindowHeight);
        while (true)
        {
            if (cWidth != Console.WindowWidth || cHeight != Console.WindowHeight)
            {
                switch (_gameEngineStatus)
                {
                    case GameEngineStatus.NotStarted:
                        _menu.OnReConfigure();
                        break;
                    case GameEngineStatus.Playing:
                        _game.Display();
                        break;
                }

                (cWidth, cHeight) = (Console.WindowWidth, Console.WindowHeight);
            }

            Thread.Sleep(50);
        }
    }
}