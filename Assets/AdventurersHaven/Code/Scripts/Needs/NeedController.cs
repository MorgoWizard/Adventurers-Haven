using UnityEngine;

public class NeedController : MonoBehaviour
{
    [SerializeField] private NeedSettings _hungerSettings, _thirstSettings, _energySettings;
    private Need _hunger, _thirst, _energy;

    private void Awake()
    {
        _hunger = new Need(_hungerSettings);
        _thirst = new Need(_thirstSettings);
        _energy = new Need(_energySettings);
    }

    private void FixedUpdate()
    {
        _hunger.ApplyConsumption(Time.fixedDeltaTime);
        _thirst.ApplyConsumption(Time.fixedDeltaTime);
        _energy.ApplyConsumption(Time.fixedDeltaTime);
        
        Debug.Log($"H: {_hunger.CurrentValue} T: {_thirst.CurrentValue} E: {_energy.CurrentValue}");
    }
}
