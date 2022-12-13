namespace SuperGame;

public class EmptyItem: GameItem
{
    public EmptyItem() : base(' ', ConsoleColor.Black)
    {
    }

    public override string Display(int sideSize)
    {
        throw new NotImplementedException();
    }

    public override void Move(int dx, int dy)
    {
        throw new NotImplementedException();
    }
}