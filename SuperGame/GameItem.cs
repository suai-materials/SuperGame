namespace SuperGame;

public abstract class GameItem : ICloneable
{
    public (int x, int y) Position = (0, 0);
    public char DisplayChar;
    public ConsoleColor Color;
    public int[,] Board;
    
    public abstract string Display(int sideSize);
    
    public abstract void Move(int dx, int dy);

    public GameItem(char displayChar, ConsoleColor color)
    {
        (DisplayChar, Color) = (displayChar, color);
    }

    public void PlaceOnBoard(int x, int y, int[,] board)
    {
        // if (x > board.Length)
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}