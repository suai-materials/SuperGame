namespace SuperGame;

public static class Constants
{
    public const byte MAX_MOVES = 3;
    public const byte POLE_W = 5;
    public const byte POLE_H = 6;

    public static string drawHeart()
    {
        string heart = String.Empty;
        Console.WriteLine("\n ██ ██ \n███████\n███████\n █████ \n   █");
        Console.ReadKey();
        return heart;
    }
}