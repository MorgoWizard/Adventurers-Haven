using UnityEngine;

public class Ship
{
    private float _baseSpeed = 15f;
    private Vector2 _direction = Vector2.right;
    private Vector2 _position = Vector2.zero;

    public Ship()
    {
        
    }
    
    public Ship(Vector2 direction, Vector2 position, float speed)
    {
        _direction = direction;
        _position = position;
        _baseSpeed = speed;
    }

    public void Move(Vector2 moveVector)
    {
        _position += moveVector;
    }

    public void Rotate(float angle)
    {
        _direction = Quaternion.AngleAxis(angle, -Vector3.forward) * _direction;
    }

    public float GetBaseSpeed()
    {
        return _baseSpeed;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    public Vector2 GetPosition()
    {
        return _position;
    }
}
