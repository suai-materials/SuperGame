﻿using System.Diagnostics;

namespace SuperGame;

public class GameEngine
{
    private GameStatus _gameStatus = GameStatus.NotStarted;
    private Menu _menu;
    private Game _game;

    private Task consoleSizeTask;

    public GameStatus GameStatus
    {
        get => _gameStatus;
        set
        {
            _gameStatus = value;
            switch (value)
            {
                case GameStatus.Quit:
                    // Сохраняем что-то
                    break;
                case GameStatus.Playing:
                    _menu.Finish();
                    _game = new Game(this);
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
                GameStatus = GameStatus.End;
            }
            else if (value > Constants.MAX_MOVES)
                _moves = Constants.MAX_MOVES;

            _moves = value;
        }
    }

    public GameEngine()
    {
        consoleSizeTask = Task.Run(() => CheckConsoleSize());
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
                switch (_gameStatus)
                {
                    case GameStatus.NotStarted:
                        _menu.onReConfigure();
                        break;
                    case GameStatus.Playing:
                        _game.Display();
                        break;
                }

                (cWidth, cHeight) = (Console.WindowWidth, Console.WindowHeight);
            }

            Thread.Sleep(50);
        }
    }
}