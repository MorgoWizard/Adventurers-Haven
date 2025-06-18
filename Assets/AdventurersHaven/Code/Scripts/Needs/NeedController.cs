
using System;
using UnityEngine;

public class NeedController : MonoBehaviour
{
    [SerializeField] private NeedSettings _hungerSettings, _thirstSettings, _energySettings;

    [SerializeField] private NeedBar _hungerBar, _thirstBar;
    private Need _hunger, _thirst, _energy;

    private void Awake()
    {
        _hunger = new Need(_hungerSettings);
        _thirst = new Need(_thirstSettings);
        _energy = new Need(_energySettings);

        _hunger.OnValueChanged += _hungerBar.SetFillAmount;
        _thirst.OnValueChanged += _thirstBar.SetFillAmount;
    }

    private void FixedUpdate()
    {
        _hunger.ApplyConsumption(Time.fixedDeltaTime);
        _thirst.ApplyConsumption(Time.fixedDeltaTime);
        _energy.ApplyConsumption(Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        ConsumableItem.OnConsume += ConsumableItemOnOnConsume;
    }
    
    private void OnDisable()
    {
        ConsumableItem.OnConsume -= ConsumableItemOnOnConsume;
    }

    private void ConsumableItemOnOnConsume(NeedType needType, float restoreAmount)
    {
        switch (needType)
        {
            case NeedType.Hunger:
                _hunger.Change(restoreAmount);
                break;
            case NeedType.Thirst:
                _thirst.Change(restoreAmount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(needType), needType, null);
        }
    }
}
