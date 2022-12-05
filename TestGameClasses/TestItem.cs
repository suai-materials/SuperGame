using System;
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
        BasicItem item = new BasicItem('X');
        Assert.AreEqual(item.Display(1), "X");
        BasicItem item2 = new BasicItem('*');
        Assert.AreEqual(item2.Display(1), "*");
        Assert.AreEqual(item.Display(2), "XX\nXX");
    }
}