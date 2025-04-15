using UnityEngine;

public class ShipNavigationManager: MonoBehaviour
{
    private Ship _ship;
    [SerializeField] private SailManager _sailManager;
    
    [SerializeField] private SteeringWheelManager _steeringWheel;
    private WeatherController _weatherController = new();

    [SerializeField] private AnimationCurve _sailEfficiencyCurve = new AnimationCurve(
        new Keyframe(1f, 1f), // 0° - 100%
        new Keyframe(0.7f, 0.7f), // 45° - 70%
        new Keyframe(0f, 0.5f), // 90° - 50%
        new Keyframe(-0.7f, 0.2f), // 135° - 20%
        new Keyframe(-1f, 0.1f) // 180° - 10%
    );

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _ship = new Ship();
        _sailManager.Initialize(new Sail(_ship));
        
        _weatherController.Initialize();
    }

    private void FixedUpdate()
    {
        RotateShip();
        MoveShip();
        
        // Debug.Log($"{_ship.GetDirection()} / {_sailManager.Sail.GetGlobalDirection()}");
    }

    private void RotateShip()
    {
        // TODO: Add speed affect to rotation
        _ship.Rotate(_steeringWheel.RudderAngle * Time.fixedDeltaTime);
    }

    private void MoveShip()
    {
        _ship.Move(_ship.GetDirection() * (CalculateShipSpeed() * Time.fixedDeltaTime));
        
        _ship.Move(_weatherController.Stream.GetDirection() * (_weatherController.Stream.GetStrength() * Time.fixedDeltaTime));
    }

    public float CalculateShipSpeed()
    {
        // TODO: Add wind strength
        if (_ship == null)
        {
            return 0;
        }
        
        return _ship.GetBaseSpeed() * GetSailEfficiency();
    }

    public Vector2 GetWindDirection()
    {
        return _weatherController.Wind?.GetDirection() ?? Vector2.zero;
    }

    public Vector2 GetShipDirection()
    {
        return _ship.GetDirection();
    }

    public float GetSailEfficiency()
    {
        if (_sailManager.Sail == null || _weatherController.Wind == null)
        {
            return 0;
        }
        
        float dotProduct = Vector2.Dot(_weatherController.Wind.GetDirection(),
            _sailManager.Sail.GetGlobalDirection());
        
        return _sailEfficiencyCurve.Evaluate(dotProduct);
    }

    public Vector2 GetSailDirection()
    {
        return _sailManager.Sail?.GetGlobalDirection() ?? Vector2.zero;
    }
    
    private void OnDrawGizmos()
    {
        if (!enabled) return;

        if (!Application.isPlaying) return;
        
        // Настройки отображения
        float arrowLength = 3f;
        Vector3 shipPos = transform.position;

        // 1. Ветер (синий)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(shipPos, _weatherController.Wind.GetDirection().normalized * arrowLength);

        // 2. Направление корабля (желтый)
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(shipPos, _ship.GetDirection());
        
        // 3. Направление паруса (magenta)
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(shipPos, _sailManager.Sail.GetGlobalDirection());
    }
}
