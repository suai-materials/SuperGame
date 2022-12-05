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

    public override void Move(int dx, int dy)
    {
        throw new NotImplementedException();
    }
    
    public BasicItem(char displayChar, ConsoleColor color = ConsoleColor.White): base(displayChar, color)
    {
    }
    
}