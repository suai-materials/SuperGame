namespace SuperGame;

public static class Utils
{
    public static void WriteLinesOnPos(int x, int y, string text, ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
    {
        var defaultBgColor = Console.BackgroundColor;
        var defaultFgColor = Console.ForegroundColor;
        Console.BackgroundColor = bgColor;
        Console.ForegroundColor = fgColor;
        foreach (var row in text.Split('\n'))
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(row);
        }
        Console.BackgroundColor = defaultBgColor;
        Console.ForegroundColor = defaultFgColor;
    }
    
    
}