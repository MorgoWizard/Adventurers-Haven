using UnityEngine;

public class ShipNavigationManager: MonoBehaviour
{
    private ShipModel _shipModel;
    public ShipModel ShipModel => _shipModel;

    [SerializeField] private Transform _shipTransform;
    
    [SerializeField] private SailManager _sailManager;
    
    [SerializeField] private SteeringWheelManager _steeringWheel;
    [SerializeField] private WeatherManager _weatherManager;

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
        _shipModel = new ShipModel();
        _sailManager.Initialize(new Sail(_shipModel));
        
        _weatherManager.Initialize();
    }

    private void FixedUpdate()
    {
        RotateShip();
        MoveShip();
    }

    private void RotateShip()
    {
        // TODO: Add speed affect to rotation
        _shipModel.Rotate(_steeringWheel.RudderAngle * Time.fixedDeltaTime);
        _shipTransform.Rotate(Vector3.up ,_steeringWheel.RudderAngle * Time.fixedDeltaTime);
    }

    private void MoveShip()
    {
        _shipModel.Move(_shipModel.Direction * (CalculateShipSpeed() * Time.fixedDeltaTime));
    }

    public float CalculateShipSpeed()
    {
        if (_shipModel == null)
        {
            return 0;
        }
        
        return _shipModel.BaseSpeed * GetSailEfficiency();
    }

    public Vector2 GetWindDirection()
    {
        return _weatherManager.Wind?.GetDirection() ?? Vector2.zero;
    }

    public float GetSailEfficiency()
    {
        if (_sailManager.Sail == null || _weatherManager.Wind == null)
        {
            return 0;
        }
        
        float dotProduct = Vector2.Dot(_weatherManager.Wind.GetDirection(),
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
        Gizmos.DrawLine(shipPos, _weatherManager.Wind.GetDirection().normalized * arrowLength);

        // 2. Направление корабля (желтый)
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(shipPos, _shipModel.Direction);
        
        // 3. Направление паруса (magenta)
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(shipPos, _sailManager.Sail.GetGlobalDirection());
    }
}
