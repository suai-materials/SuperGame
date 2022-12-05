namespace SuperGame;

public class Game
{
    private GameEngine _gameEngine;
    public GameItem SelectedItem;

    public GameItem[] GameItems = new[]
        {new BasicItem('X', ConsoleColor.Blue), new BasicItem('Y', ConsoleColor.Yellow), new BasicItem('@')};

    public (int Width, int Height) size = (5, 5);
    private Random _random = new Random();
    public List<List<GameItem>> Board = new();
    int _cWidth, _cHeight;
    private (int x, int y) choosedItemPos = (0, 0);

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

    public void Display()
    {
        Console.Clear();
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        int side = Math.Min(_cWidth, _cHeight) / Math.Max(size.Height, size.Width);
        while (side * size.Width > _cWidth - 1 || side * size.Height > _cHeight - 1)
        {
            side--;
        }

        (int x, int y) pos = (_cWidth / 2 - side * size.Width / 2, _cHeight / 2 - side * size.Height / 2);
        var a = Console.CapsLock;
        for (int i = 0; i < size.Width; i++)
        {
            pos.x = _cWidth / 2 - side * size.Width / 2;
            for (int j = 0; j < size.Height; j++)
            {
                if (i == choosedItemPos.y && j == choosedItemPos.x)
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(side), Board[i][j].Color, ConsoleColor.White);
                }
                else
                {
                    Utils.WriteLinesOnPos(pos.x, pos.y,
                        Board[i][j].Display(side), Board[i][j].Color);
                }
                pos.x += side;
            }

            pos.y += side;
        }

        Thread.Sleep(10000);
    }
}