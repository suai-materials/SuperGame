namespace SuperGame;

public abstract class GameItem : ICloneable
{
    public (int x, int y) Position = (0, 0);
    public char DisplayChar;
    public ConsoleColor Color;
    public int[,] Board;
    
    public abstract string Display(int sideSize);
    
    public abstract void Move(int dx, int dy, int i, int dy1, Game game);

    public GameItem(char displayChar, ConsoleColor color)
    {
        (DisplayChar, Color) = (displayChar, color);
    }
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}