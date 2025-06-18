using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaveMovement : MonoBehaviour
{
    [Header("Wave Parameters")]
    [SerializeField] private float amplitude = 1f;    // Высота волн
    [SerializeField] private float frequency = 1f;    // Частота волн
    [SerializeField] private float waveSpeed = 1f;    // Скорость движения волн
    [SerializeField] private Vector2 waveDirection = Vector2.right; // Направление волн

    [Header("Advanced Settings")]
    [SerializeField] private bool usePhysics = true;  // Использовать физику
    [SerializeField] private float damping = 0.1f;    // Сопротивление

    private Rigidbody rb;
    private Vector3 startPosition;
    private float timeOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // Для вариативности
    }

    private void FixedUpdate()
    {
        Vector3 waveForce = CalculateWaveForce();
        
        if (usePhysics && rb != null)
        {
            ApplyPhysicsMovement(waveForce);
        }
        else
        {
            ApplyDirectMovement(waveForce);
        }
    }

    private Vector3 CalculateWaveForce()
    {
        // Рассчитываем положение в волновом поле
        float waveX = waveDirection.x * (startPosition.x + Time.time * waveSpeed);
        float waveZ = waveDirection.y * (startPosition.z + Time.time * waveSpeed);
        
        // Генерация волны с использованием синусоидальной функции
        float waveHeight = amplitude * Mathf.Sin((waveX + waveZ) * frequency + timeOffset);
        
        // Добавляем дополнительную волну для сложности
        float secondaryWave = amplitude * 0.5f * Mathf.Cos((waveX * 0.8f - waveZ * 1.2f) * frequency * 0.5f + timeOffset);
        
        return new Vector3(0, waveHeight + secondaryWave, 0);
    }

    private void ApplyPhysicsMovement(Vector3 force)
    {
        // Плавное применение силы с демпфированием
        rb.AddForce(force - rb.linearVelocity * damping, ForceMode.Acceleration);
        
        // Имитация вращения на основе наклона волны
        Quaternion targetRotation = Quaternion.Euler(
            force.z * 2f,
            0,
            -force.x * 2f
        );
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime));
    }

    private void ApplyDirectMovement(Vector3 force)
    {
        transform.position = startPosition + force;
        transform.rotation = Quaternion.Euler(
            force.z * 2f,
            0,
            -force.x * 2f
        );
    }

    // Редактор для визуальной настройки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(waveDirection.x, 0, waveDirection.y) * 3f);
    }
}