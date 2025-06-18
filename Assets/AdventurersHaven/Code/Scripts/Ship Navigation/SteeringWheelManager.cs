using UnityEngine;

public class SteeringWheelManager : MonoBehaviour
{
    [SerializeField] private Transform steeringWheelTransform;
    [SerializeField] private float maxRotationCount = 2.5f;
    [SerializeField] private float maxRudderAngle = 30f;
    
    private Quaternion _lastRotation;
    private float _totalRotation;
    private float _defaultRotation;
    private float _maxRotationAngle;
    
    public float RudderAngle { get; private set; }

    private void Start()
    {
        _lastRotation = steeringWheelTransform.localRotation;
        _defaultRotation = _lastRotation.eulerAngles.x;
        _maxRotationAngle = maxRotationCount * 360f;
        _totalRotation = 0f;
    }

    private void Update()
    {
        Quaternion currentRot = steeringWheelTransform.localRotation;
        
        Quaternion deltaRotation = currentRot * Quaternion.Inverse(_lastRotation);
        float deltaAngle = deltaRotation.eulerAngles.x;
        
        if (deltaAngle > 180f) deltaAngle -= 360f;
        
        _totalRotation += deltaAngle;
        
        if (Mathf.Abs(_totalRotation) > _maxRotationAngle)
        {
            _totalRotation = Mathf.Sign(_totalRotation) * _maxRotationAngle;
            float clampedAngle = _defaultRotation + _totalRotation;
            if (Mathf.Abs(Mathf.DeltaAngle(steeringWheelTransform.localEulerAngles.x, clampedAngle)) > 1)
            {
                steeringWheelTransform.localRotation = Quaternion.Euler(
                    clampedAngle,
                    currentRot.eulerAngles.y,
                    currentRot.eulerAngles.z
                );
            }

        }
        
        RudderAngle = maxRudderAngle * (_totalRotation / _maxRotationAngle);
        
        _lastRotation = currentRot;
    }
}