namespace SuperGame;

public interface IScreen
{
    void CheckInput();
    void Display();
    void Finish();
    void OnReConfigure();

    void Start();
}