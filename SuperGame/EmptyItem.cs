namespace SuperGame;

public class EmptyItem: BasicItem
{
    public EmptyItem() : base(' ', ConsoleColor.Black)
    {
    }

    public override string Display(int sideSize)
    {
        return base.Display(sideSize);
    }

    public override void Move(int dx, int dy, int i, int dy1, Game game)
    {
        throw new NotImplementedException();
    }
}