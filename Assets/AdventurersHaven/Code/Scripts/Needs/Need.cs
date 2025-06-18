using System;

public class Need
{
    private float _currentValue;
    private readonly float _maxValue = 100f;
    private readonly float _minValue;
    
    public event Action<float> OnValueChanged;

    private readonly float _consumptionPerSecond;
    
    public float CurrentValue
    {
        get => _currentValue;
        private set
        {
            float newValue = Math.Clamp(value, _minValue, _maxValue);
     
            _currentValue = newValue;
            OnValueChanged?.Invoke(_currentValue/_maxValue);
        }
    }
    
    public Need()
    {
        _currentValue = _maxValue;
    }

    public Need(NeedSettings needSettings)
    {
        _maxValue = needSettings.MaxValue;
        _minValue = needSettings.MinValue;
        _consumptionPerSecond = needSettings.ConsumptionPerSecond;

        _currentValue = _maxValue;
    }
    
    public void Change(float amount)
    {
        CurrentValue += amount;
    }

    public void ApplyConsumption(float deltaTime)
    {
        Change(-_consumptionPerSecond * deltaTime);
    }
}