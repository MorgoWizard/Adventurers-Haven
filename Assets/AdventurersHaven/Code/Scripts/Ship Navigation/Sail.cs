using UnityEngine;

public class Sail
{
    private readonly ShipModel _shipModel;
    private float _currentAngle;
    private readonly float _maxRotationAngle = 60f;

    public Sail(ShipModel shipModel)
    {
        _shipModel = shipModel;
    }
    
    public Sail(ShipModel shipModel, float maxRotationAngle)
    {
        _shipModel = shipModel;
        _maxRotationAngle = maxRotationAngle;
    }

    public void Rotate(float deltaAngle)
    {
        _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, -_maxRotationAngle, _maxRotationAngle);
    }

    public Vector2 GetGlobalDirection()
    {
        Vector2 shipDirection = _shipModel.Direction;
        
        float shipAngle = Vector2.SignedAngle(Vector2.right, shipDirection);
        shipAngle = (shipAngle + 360f) % 360f;
        
        float totalAngle = shipAngle - _currentAngle;
        
        return new Vector2(
            Mathf.Cos(totalAngle * Mathf.Deg2Rad),
            Mathf.Sin(totalAngle * Mathf.Deg2Rad)
        ).normalized;
    }

    public float GetCurrentSailAngle()
    {
        return _currentAngle;
    }
}