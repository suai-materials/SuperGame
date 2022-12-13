namespace SuperGame;

public static class Constants
{
    public const byte MAX_MOVES = 3;
    public const byte POLE_W = 5;
    public const byte POLE_H = 6;

    public static readonly List<ConsoleKey> UsedKeys = new()
    {
        ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.A, ConsoleKey.D,
        ConsoleKey.RightArrow, ConsoleKey.LeftArrow
    };

    public static readonly Dictionary<ConsoleColor, ConsoleColor> BackGroundColors = new()
    {
        {ConsoleColor.White, ConsoleColor.DarkGray},
        {ConsoleColor.DarkBlue, ConsoleColor.White},
        {ConsoleColor.Yellow, ConsoleColor.Blue},
        {ConsoleColor.Blue, ConsoleColor.Yellow},
        {ConsoleColor.Magenta, ConsoleColor.Cyan},
        {ConsoleColor.Cyan, ConsoleColor.Magenta},
    };

    public static string drawHeart()
    {
        string heart = String.Empty;
        Console.WriteLine("\n ██ ██ \n███████\n███████\n █████ \n   █");
        Console.ReadKey();
        return heart;
    }
}