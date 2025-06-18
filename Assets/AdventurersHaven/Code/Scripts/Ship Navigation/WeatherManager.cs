using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Transform _windParticles;
    
    private WeatherPhenomenon _wind, _stream;
    
    public WeatherPhenomenon Wind => _wind;
    public WeatherPhenomenon Stream => _stream;
    
    public void Initialize()
    {
        _wind = new WeatherPhenomenon();
        _stream = new WeatherPhenomenon();

        _timer.OnComplete += UpdateWind;
    }

    private void UpdateWind()
    {
        _wind.SetRandomDirection();
        _windParticles.rotation = RotateTowards2D(_wind.GetDirection());
    }
    
    public Quaternion RotateTowards2D(Vector2 direction)
    {
        Vector3 dir3D = new Vector3(direction.x, 0f, direction.y);
        return Quaternion.LookRotation(dir3D);
    }

}
