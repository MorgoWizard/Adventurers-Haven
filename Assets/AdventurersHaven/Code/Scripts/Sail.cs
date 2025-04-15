using UnityEngine;

public class Sail
{
    private readonly Ship _ship;
    private float _currentAngle = 0f;
    private float _maxRotationAngle = 60f;

    public Sail(Ship ship)
    {
        _ship = ship;
    }
    
    public Sail(Ship ship, float maxRotationAngle)
    {
        _ship = ship;
        _maxRotationAngle = maxRotationAngle;
    }

    public void Rotate(float deltaAngle)
    {
        // Ограничиваем угол в допустимых пределах
        _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, -_maxRotationAngle, _maxRotationAngle);
    }

    public Vector2 GetGlobalDirection()
    {
        // Получаем базовое направление корабля
        Vector2 shipDirection = _ship.GetDirection();
        
        // Вычисляем угол корабля (0-360)
        float shipAngle = Vector2.SignedAngle(Vector2.right, shipDirection);
        shipAngle = (shipAngle + 360f) % 360f;
        
        // Вычисляем итоговый угол паруса (корабль + отклонение паруса)
        float totalAngle = shipAngle - _currentAngle;
        
        // Преобразуем угол в вектор направления
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