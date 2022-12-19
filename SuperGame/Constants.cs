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

    
    
}