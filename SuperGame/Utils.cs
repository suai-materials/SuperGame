namespace SuperGame;

public static class Utils
{
    public static ConsoleColor defaultBgColor;
    public static ConsoleColor defaultFgColor;
    
    public static void WriteLinesOnPos(int x, int y, string text, ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
    {
        
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