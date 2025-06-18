using UnityEngine;
using NUnit.Framework;

public class ShipModelTests
{
    private ShipModel _shipModel;
    private ShipModel _customShipModel; // Для теста кастомного конструктора

    [SetUp]
    public void Setup()
    {
        _shipModel = new ShipModel();
        _customShipModel = new ShipModel(Vector2.up, new Vector2(10f, 5f), 20f);
    }

    [TearDown]
    public void Teardown()
    {
        _shipModel = null;
        _customShipModel = null;
        
        // Дополнительная очистка, если использовались другие ресурсы
        // Например, если бы мы работали с MonoBehaviour:
        // Object.DestroyImmediate(testGameObject);
    }

    [Test]
    public void Ship_DefaultConstructor_SetsDefaultValues()
    {
        Assert.AreEqual(Vector2.right, _shipModel.Direction);
        Assert.AreEqual(Vector2.zero, _shipModel.Position);
        Assert.AreEqual(15f, _shipModel.BaseSpeed);
    }

    [Test]
    public void Ship_CustomConstructor_SetsCorrectValues()
    {
        Assert.AreEqual(Vector2.up, _customShipModel.Direction);
        Assert.AreEqual(new Vector2(10f, 5f), _customShipModel.Position);
        Assert.AreEqual(20f, _customShipModel.BaseSpeed);
    }
    
    [Test]
    public void Move_ChangesPositionCorrectly()
    {
        _shipModel.Move(new Vector2(5f, 3f));
        Assert.AreEqual(new Vector2(5f, 3f), _shipModel.Position);

        _shipModel.Move(new Vector2(-2f, 1f));
        Assert.AreEqual(new Vector2(3f, 4f), _shipModel.Position);
    }

    [Test]
    public void Rotate_PositiveAngle_RotatesClockwise()
    {
        _shipModel.Rotate(90f);
        Assert.That(_shipModel.Direction.x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_shipModel.Direction.y, Is.EqualTo(-1f).Within(0.001f));
    }

    [Test]
    public void Rotate_NegativeAngle_RotatesCounterClockwise()
    {
        _shipModel.Rotate(-90f);
        Assert.That(_shipModel.Direction.x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_shipModel.Direction.y, Is.EqualTo(1f).Within(0.001f));
    }

    [Test]
    public void Rotate_MultipleRotations_AccumulatesCorrectly()
    {
        _shipModel.Rotate(45f);
        _shipModel.Rotate(45f); // Общий поворот 90°
        
        Debug.Log(_shipModel.Direction);
        
        Assert.That(_shipModel.Direction.x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(_shipModel.Direction.y, Is.EqualTo(-1f).Within(0.001f));
    }

    [Test]
    public void Rotate_360_ReturnsToOriginalDirection()
    {
        var originalDirection = _shipModel.Direction;
        _shipModel.Rotate(360f);
        
        Assert.That(_shipModel.Direction.x, Is.EqualTo(originalDirection.x).Within(0.001f));
        Assert.That(_shipModel.Direction.y, Is.EqualTo(originalDirection.y).Within(0.001f));
    }

    [Test]
    public void GetBaseSpeed_ReturnsCorrectValue()
    {
        Assert.AreEqual(15f, _shipModel.BaseSpeed);
        
        var fastShip = new ShipModel(Vector2.right, Vector2.zero, 25f);
        Assert.AreEqual(25f, fastShip.BaseSpeed);
    }
}