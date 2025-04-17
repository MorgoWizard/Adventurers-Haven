using UnityEngine;

public class SteeringWheelManager : MonoBehaviour
{
    [SerializeField] private Transform steeringWheelTransform; // Трансформ штурвала
    
    private float _lastRotation; // Последний угол поворота
    private float _maxRotationAngle;
    
    [SerializeField] private float maxRotationCount;
    private float _rotationCount; // Количество полных оборотов
    
    private float _defaultRotation;

    [SerializeField] private float maxRudderAngle;
    public float RudderAngle { get; private set; }

    private void Start()
    {
        _lastRotation = steeringWheelTransform.eulerAngles.z; // Инициализируем начальный угол
        _defaultRotation = _lastRotation;
        _maxRotationAngle = maxRotationCount * 360f;
    }

    private void Update()
    {
        // Получаем текущий угол поворота штурвала по оси Z
        float currentRotation = steeringWheelTransform.eulerAngles.z;

        // Находим разницу между текущим и предыдущим углом
        float deltaRotation = currentRotation - _lastRotation;

        switch (deltaRotation)
        {
            // Учитываем переход через 360 градусов
            case > 180f:
                deltaRotation -= 360f; // Переход по часовой стрелке
                break;
            case < -180f:
                deltaRotation += 360f; // Переход против часовой стрелки
                break;
        }
        
        // Добавляем изменение угла к общему количеству оборотов
        _rotationCount += deltaRotation / 360f;
        
        RudderAngle = maxRudderAngle * (_rotationCount / maxRotationCount);
        
        // ограничение поворота при достижении максимального угла
        if (_rotationCount > maxRotationCount)
        {
            steeringWheelTransform.eulerAngles = new Vector3(steeringWheelTransform.eulerAngles.x,steeringWheelTransform.eulerAngles.y, _defaultRotation + _maxRotationAngle);
        }
        else if (_rotationCount < -maxRotationCount)
        {
            steeringWheelTransform.eulerAngles = new Vector3(steeringWheelTransform.eulerAngles.x,steeringWheelTransform.eulerAngles.y, _defaultRotation - _maxRotationAngle);
        }

        // Обновляем последний угол
        _lastRotation = currentRotation;
    }

    public float GetRotationCount()
    {
        return _rotationCount; // Возвращаем количество оборотов
    }
}