using UnityEngine;

public class SailManager : MonoBehaviour
{
    [SerializeField] private float _sailRotationSpeed = 15f;
    [SerializeField] private Transform _sailTransform;
    
    private Sail _sail;
    public Sail Sail => _sail;
    
    private bool _isRotating = false;
    
    private float _speedModifier = 1f;

    public void Initialize(Sail newSail)
    {
        _sail = newSail;
    }

    private void Update()
    {
        if (_isRotating)
        {
            _sail.Rotate(_sailRotationSpeed * Time.deltaTime * _speedModifier);
            _sailTransform.localEulerAngles = new Vector3(_sailTransform.localEulerAngles.x, _sail.GetCurrentSailAngle(), _sailTransform.localEulerAngles.z);
        }
    }
    
    public void RotateLeft()
    {
        _isRotating = true;
        _speedModifier = -1f;
    }

    public void RotateRight()
    {
        _isRotating = true;
        _speedModifier = 1f;
    }

    public void StopRotating()
    {
        _isRotating = false;
    }
}