using NUnit.Framework;
using SuperGame;

namespace TestGameClasses;

public class TestItem
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GameItemTest()
    {
        GameItem item = new GameItem('X');
        Assert.AreEqual(item.Display(), "X");
        GameItem item2 = new GameItem('*');
        Assert.AreEqual(item2.Display(), "*");
        item.SideSize = 2;
        Assert.AreEqual(item.Display(), "XX\nXX");
    }
}