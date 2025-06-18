using UnityEngine;

public class ShipModel
{
    public float BaseSpeed { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 Position { get; private set; }

    public ShipModel()
    {
        BaseSpeed = 2f;
        Direction = Vector2.right;
        Position = Vector2.zero;
    }
    
    public ShipModel(Vector2 startPosition, Vector2 startDirection, float speed)
    {
        Position = startPosition;
        Direction = startDirection;
        BaseSpeed = speed;
    }

    public void Move(Vector2 moveVector)
    {
        Position += moveVector;
    }

    public void Rotate(float angle)
    {
        Direction = Quaternion.AngleAxis(angle, -Vector3.forward) * Direction;
    }
}
