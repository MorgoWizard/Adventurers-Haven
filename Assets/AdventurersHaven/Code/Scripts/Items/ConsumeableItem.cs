using System;
using UnityEngine;

public enum NeedType
{
    Hunger,    
    Thirst     
}

[RequireComponent(typeof(Collider))]
public abstract class ConsumableItem : MonoBehaviour, IConsumable
{
    public static event Action<NeedType, float> OnConsume;
    
    [SerializeField] protected NeedType _needType;
    [SerializeField] protected float _restoreAmount;
    
    public NeedType NeedType => _needType;
    public float RestoreAmount => _restoreAmount;
    
    public virtual void Consume()
    {
        OnConsume?.Invoke(_needType, _restoreAmount);

        Destroy(gameObject);
    }
}