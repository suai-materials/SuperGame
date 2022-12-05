using System.Threading;
using System.Threading.Tasks;

namespace SuperGame;

public class Menu: IScreen
{
    public List<MenuItem> MenuItems = new ()
        {new MenuItem("Начать играть", GameStatus.Playing), new MenuItem("Таблица Лидеров", GameStatus.LeaderBoard),new MenuItem("Выйти", GameStatus.Quit)};

    public string Logo = "";
    private GameEngine _gameEngine;
    MenuItem _chosenItem;
    int _cWidth, _cHeight;
    (int X, int Y) _pos = (0, 0);
    private Task _inputTask;
    private bool _isFinish;


    public Menu(GameEngine gameEngine)
    {
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        _chosenItem = MenuItems.First();
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
        _inputTask = Task.Run(() => CheckInput());
        _inputTask.Wait();
    }

    public void Finish()
    {
        _isFinish = true;
    }

    public void OnReConfigure()
    {
        Display();
    }
    public void CheckInput()
    {
        while (_gameEngine.GameStatus == GameStatus.NotStarted)
        {

            while (!Console.KeyAvailable)
            {
                if (_isFinish) return;
            }


            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (_chosenItem == MenuItems.Last())
                        _chosenItem = MenuItems.First();
                    else
                    {
                        _chosenItem = MenuItems[MenuItems.IndexOf(_chosenItem) + 1];
                    }

                    DisplayMenu();
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (_chosenItem == MenuItems.First())
                        _chosenItem = MenuItems.Last();
                    else
                    {
                        _chosenItem = MenuItems[MenuItems.IndexOf(_chosenItem) - 1];
                    }

                    DisplayMenu();
                    break;
                case ConsoleKey.Enter:
                    _gameEngine.GameStatus = _chosenItem.Action;
                    return;
                    break;
            }
        }
    }


    public void Display()
    {
        (_cWidth, _cHeight) = (Console.WindowWidth, Console.WindowHeight);
        // Отображение логотипа
        Console.Clear();
        _pos = (_cWidth / 2 - (Logo.Split('\n').MaxBy(s => s.Length) ?? "").Length / 2, _cHeight / 10);
        
        Utils.WriteLinesOnPos(_pos.X, _pos.Y, Logo);

        DisplayMenu();
    }

    void DisplayMenu()
    {
        // Отображение вариантов выбора в меню
        var defaultBgColor = Console.BackgroundColor;
        var defaultFgColor = Console.ForegroundColor;

        _pos = (0, _cHeight / 2);
        foreach (var item in MenuItems)
        {
            Console.SetCursorPosition(_cWidth / 2 - item.Name.Length / 2, _pos.Y++);
            if (item == _chosenItem)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(item.Name);
                (Console.BackgroundColor, Console.ForegroundColor) = (defaultBgColor, defaultFgColor);
            }
            else
            {
                Console.WriteLine(item.Name);
            }
        }

        Console.SetCursorPosition(_cWidth - 1, _cHeight - 2);
    }
}