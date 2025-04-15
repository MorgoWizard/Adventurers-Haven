using UnityEngine;
using NUnit.Framework;

public class ShipTests
{
    private Ship _ship;
    private Ship _customShip; // Для теста кастомного конструктора

    [SetUp]
    public void Setup()
    {
        _ship = new Ship();
        _customShip = new Ship(Vector2.up, new Vector2(10f, 5f), 20f);
    }

    [TearDown]
    public void Teardown()
    {
        _ship = null;
        _customShip = null;
        
        // Дополнительная очистка, если использовались другие ресурсы
        // Например, если бы мы работали с MonoBehaviour:
        // Object.DestroyImmediate(testGameObject);
    }

    [Test]
    public void Ship_DefaultConstructor_SetsDefaultValues()
    {
        Assert.AreEqual(Vector2.right, _ship.GetDirection());
        Assert.AreEqual(Vector2.zero, _ship.GetPosition());
        Assert.AreEqual(15f, _ship.GetBaseSpeed());
    }

    [Test]
    public void Ship_CustomConstructor_SetsCorrectValues()
    {
        Assert.AreEqual(Vector2.up, _customShip.GetDirection());
        Assert.AreEqual(new Vector2(10f, 5f), _customShip.GetPosition());
        Assert.AreEqual(20f, _customShip.GetBaseSpeed());
    }
    
    [Test]
    public void Move_ChangesPositionCorrectly()
    {
        _ship.Move(new Vector2(5f, 3f));
        Assert.AreEqual(new Vector2(5f, 3f), _ship.GetPosition());

        _ship.Move(new Vector2(-2f, 1f));
        Assert.AreEqual(new Vector2(3f, 4f), _ship.GetPosition());
    }

    [Test]
    public void Rotate_PositiveAngle_RotatesClockwise()
    {
        _ship.Rotate(90f);
        Assert.That(_ship.GetDirection().x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_ship.GetDirection().y, Is.EqualTo(-1f).Within(0.001f));
    }

    [Test]
    public void Rotate_NegativeAngle_RotatesCounterClockwise()
    {
        _ship.Rotate(-90f);
        Assert.That(_ship.GetDirection().x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_ship.GetDirection().y, Is.EqualTo(1f).Within(0.001f));
    }

    [Test]
    public void Rotate_MultipleRotations_AccumulatesCorrectly()
    {
        _ship.Rotate(45f);
        _ship.Rotate(45f); // Общий поворот 90°
        
        Debug.Log(_ship.GetDirection());
        
        Assert.That(_ship.GetDirection().x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_ship.GetDirection().y, Is.EqualTo(-1f).Within(0.001f));
    }

    [Test]
    public void Rotate_360_ReturnsToOriginalDirection()
    {
        var originalDirection = _ship.GetDirection();
        _ship.Rotate(360f);
        
        Assert.That(_ship.GetDirection().x, Is.EqualTo(originalDirection.x).Within(0.001f));
        Assert.That(_ship.GetDirection().y, Is.EqualTo(originalDirection.y).Within(0.001f));
    }

    [Test]
    public void GetBaseSpeed_ReturnsCorrectValue()
    {
        Assert.AreEqual(15f, _ship.GetBaseSpeed());
        
        var fastShip = new Ship(Vector2.right, Vector2.zero, 25f);
        Assert.AreEqual(25f, fastShip.GetBaseSpeed());
    }
}