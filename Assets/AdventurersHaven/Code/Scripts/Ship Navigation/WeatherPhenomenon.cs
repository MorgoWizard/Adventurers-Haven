using UnityEngine;

public class WeatherPhenomenon
{
    private Vector2 _direction;
    private const float Strength = 5f;

    public WeatherPhenomenon()
    {
        SetRandomDirection();
    }

    private void SetDirection(Vector2 newDirection)
    {
        _direction = newDirection;
    }

    private Vector2 RandomizeDirection()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    public void SetRandomDirection()
    {
        SetDirection(RandomizeDirection());
    }

    public float GetStrength()
    {
        return Strength;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }
}
