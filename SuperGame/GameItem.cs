namespace SuperGame;

public class GameItem
{
    public readonly char DisplayChar;
    public (int x, int y) Position = (0, 0);
    public int SideSize = 1;

    public string Display()
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

    public GameItem(char displayChar)
    {
        DisplayChar = displayChar;
    }
}