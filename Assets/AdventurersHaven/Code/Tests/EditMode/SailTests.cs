using UnityEngine;
using NUnit.Framework;

public class SailTests
{
    private Ship _ship;
    private Sail _sail;

    [SetUp]
    public void Setup()
    {
        _ship = new Ship();
        _sail = new Sail(_ship);
    }

    [TearDown]
    public void Teardown()
    {
        _sail = null;
        _ship = null;
    }

    [Test]
    public void Sail_InitialState_HasCorrectDefaults()
    {
        Assert.AreEqual(_ship.GetDirection(), _sail.GetGlobalDirection());
        Assert.AreEqual(0f, _sail.GetCurrentSailAngle());
    }

    [Test]
    public void Rotate_PositiveAngle_ChangesDirection()
    {
        _sail.Rotate(45f);
        Assert.AreEqual(45f, _sail.GetCurrentSailAngle(), 0.001f);
        
        Vector2 expectedDir = new Vector2(0.707f, -0.707f);
        Assert.AreEqual(expectedDir.x, _sail.GetGlobalDirection().x, 0.01f);
        Assert.AreEqual(expectedDir.y, _sail.GetGlobalDirection().y, 0.01f);
    }

    [Test]
    public void Rotate_NegativeAngle_ChangesDirection()
    {
        _sail.Rotate(-45f);
        Assert.AreEqual(-45f, _sail.GetCurrentSailAngle(), 0.001f);
        
        Vector2 expectedDir = new Vector2(0.707f, 0.707f); // cos(-45°), sin(-45°)
        Assert.AreEqual(expectedDir.x, _sail.GetGlobalDirection().x, 0.01f);
        Assert.AreEqual(expectedDir.y, _sail.GetGlobalDirection().y, 0.01f);
    }

    [Test]
    public void Rotate_ExceedsMaxAngle_ClampsCorrectly()
    {
        // Пытаемся повернуть на 70° (максимум 60°)
        _sail.Rotate(70f);
        
        Assert.That(_sail.GetCurrentSailAngle(), Is.EqualTo(60f).Within(0.001f));
        
        // Пытаемся повернуть на -80° (минимум -60°)
        _sail.Rotate(-140f); // 60 - 140 = -80 → должно заклиппиться на -60
        
        Assert.That(_sail.GetCurrentSailAngle(), Is.EqualTo(-60f).Within(0.001f));
    }

    [Test]
    public void GetGlobalDirection_AfterShipRotation_Correct()
    {
        // Поворачиваем корабль на 90°
        _ship.Rotate(90f);
        
        // Парус без локального поворота должен смотреть вверх
        Vector2 expectedDirection = Vector2.down;
        Vector2 actualDirection = _sail.GetGlobalDirection();
        
        Assert.That(actualDirection.x, Is.EqualTo(expectedDirection.x).Within(0.001f));
        Assert.That(actualDirection.y, Is.EqualTo(expectedDirection.y).Within(0.001f));
    }
}