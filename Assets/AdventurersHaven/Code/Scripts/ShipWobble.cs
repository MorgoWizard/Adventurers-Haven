using UnityEngine;

public class ShipWobble : MonoBehaviour
{
    [SerializeField] private float _rotationAmplitude = 2f; // амплитуда вращения (в градусах)
    [SerializeField] private float _rotationSpeed = 1f;     // скорость вращения
    [SerializeField] private float _positionAmplitude = 0.2f; // амплитуда по оси Y
    [SerializeField] private float _positionSpeed = 1f;       // скорость покачивания по Y

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        // Смещение по Y
        float yOffset = Mathf.Sin(Time.time * _positionSpeed) * _positionAmplitude;
        transform.localPosition = _startPosition + Vector3.up * yOffset;

        // Качка по Z
        float zRotation = Mathf.Sin(Time.time * _rotationSpeed) * _rotationAmplitude;
        transform.localRotation = _startRotation * Quaternion.Euler(0f, 0f, zRotation);
    }
}