using UnityEngine;

public class WeatherPhenomenon
{
    public enum WeatherType
    {
        DEFAULT,
        STORM,
        STILL
    }
    
    private Vector2 _direction;
    private float _strength = 5f;

    private WeatherType currentWeather = WeatherType.DEFAULT;

    public WeatherPhenomenon()
    {
        SetDirection(RandomizeDirection());
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

    public float GetStrength()
    {
        return _strength;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }
}
