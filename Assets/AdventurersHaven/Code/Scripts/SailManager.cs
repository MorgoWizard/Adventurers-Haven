using UnityEngine;

public class SailManager : MonoBehaviour
{
    [SerializeField] private Transform sailTransform; // Трансформ паруса
    
    private Sail _sail;
    public Sail Sail => _sail;
    
    private float _lastRotation;

    private void Start()
    {
        _lastRotation = sailTransform.eulerAngles.z; // Инициализируем начальный угол
    }

    public void Initialize(Sail newSail)
    {
        _sail = newSail;
    }

    private void Update()
    {
        // Получаем текущий угол поворота штурвала по оси Z
        float currentRotation = sailTransform.eulerAngles.z;

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
        
        if(_sail != null && deltaRotation != 0) _sail.Rotate(deltaRotation);

        // Обновляем последний угол
        _lastRotation = currentRotation;
    }
}