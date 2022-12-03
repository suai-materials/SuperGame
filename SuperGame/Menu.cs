using System.Threading;
using System.Threading.Tasks;

namespace SuperGame;

public class Menu
{
    public Dictionary<string, GameStatus> MenuItems = new Dictionary<string, GameStatus>()
        {{"Начать играть", GameStatus.Playing}, {"Выйти", GameStatus.Quit}};

    public string Logo = "";
    private GameEngine _gameEngine;
    KeyValuePair<string, GameStatus> chosenItem;
    int cWidth, cHeight;
    (int X, int Y) pos = (0, 0);
    private Task inputTask;
    private bool _isFinish = false;


    public Menu(GameEngine gameEngine)
    {
        (cWidth, cHeight) = (Console.WindowWidth, Console.WindowHeight);
        chosenItem = MenuItems.First();
        try
        {
            Logo = File.ReadAllText("res\\Hello.txt");
        }
        catch (FileNotFoundException e)
        {
            Logo = "Поиграем без логотипа";
        }

        _gameEngine = gameEngine;

    }

    public void Start()
    {
        Display();
        inputTask = Task.Run(() => CheckInput());
        inputTask.Wait();
    }

    public void Finish()
    {
        _isFinish = true;
    }

    public void onReConfigure()
    {
        Display();
    }

    private void CheckInput()
    {
        while (_gameEngine.GameStatus == GameStatus.NotStarted)
        {

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(10);
                if (_isFinish) return;
            }


            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (chosenItem.Key == MenuItems.First().Key)
                        chosenItem = MenuItems.Last();
                    else
                    {
                    }

                    DisplayMenu();
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (chosenItem.Key == MenuItems.Last().Key)
                        chosenItem = MenuItems.First();
                    else
                    {
                    }

                    DisplayMenu();
                    break;
            }
        }
    }


    private void Display()
    {
        (cWidth, cHeight) = (Console.WindowWidth, Console.WindowHeight);
        // Отображение логотипа
        Console.Clear();
        pos = (cWidth / 2 - (Logo.Split('\n').MaxBy(s => s.Length) ?? "").Length / 2, cHeight / 10);

        foreach (var row in Logo.Split('\n'))
        {
            Console.SetCursorPosition(pos.X, pos.Y++);
            Console.WriteLine(row);
        }

        DisplayMenu();
    }

    void DisplayMenu()
    {
        // Отображение вариантов выбора в меню
        var defaultBgColor = Console.BackgroundColor;
        var defaultFgColor = Console.ForegroundColor;

        pos = (0, cHeight / 2);
        foreach (var item in MenuItems)
        {
            Console.SetCursorPosition(cWidth / 2 - item.Key.Length / 2, pos.Y++);
            if (item.Key == chosenItem.Key)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(item.Key);
                (Console.BackgroundColor, Console.ForegroundColor) = (defaultBgColor, defaultFgColor);
            }
            else
            {
                Console.WriteLine(item.Key);
            }
        }

        Console.SetCursorPosition(cWidth - 1, cHeight - 2);
    }
}