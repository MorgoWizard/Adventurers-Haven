public class WeatherController
{
    private WeatherPhenomenon _wind, _stream;
    
    public WeatherPhenomenon Wind => _wind;
    public WeatherPhenomenon Stream => _stream;
    
    public void Initialize()
    {
        _wind = new WeatherPhenomenon();
        _stream = new WeatherPhenomenon();
    }
}
