namespace SuperGame;

public class BasicItem : GameItem
{

    public override string Display(int SideSize)
    {
        string result = "";
        for (int i = 0; i < SideSize; i++)
        {
            result += new string(DisplayChar, SideSize);
            // Убираем последний перенос строки, чтобы не ломать систему, лишним переносом
            if (i != SideSize - 1)
                result += '\n';
        }
        return result;
    }

    public override void Move(int x, int y, int dx, int dy, Game game)
    {
        if ((dy < 0 && y == 0) || (dx < 0 && x == 0) || (dx > 0 && x == game.Board.Count - 1) ||
            (dy > 0 && y == game.Board[0].Count - 1))
        {
            game.Display();
            return;
        }

        var saveItem = game.Board[x + dx][y + dy];
        game.Board[x + dx][y + dy] = this;
        game.Board[x][y] = saveItem;
        game.Process();
        game.Display();
    }
    
    public BasicItem(char displayChar,  ConsoleColor color = ConsoleColor.White): base(displayChar, color)
    {
    }
    
}