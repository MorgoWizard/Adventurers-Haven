using System;

public interface IConsumable
{
    static event Action<NeedType, float> OnConsume;
    
    NeedType NeedType { get; }          
    float RestoreAmount { get; }       
    void Consume();                     
}