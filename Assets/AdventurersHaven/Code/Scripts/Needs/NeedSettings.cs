using UnityEngine;

[CreateAssetMenu(fileName = "SO_NeedSettings_NeedName", menuName = "Needs/NeedSettings")]
public class NeedSettings : ScriptableObject
{
    public float MaxValue;
    public float MinValue;
    public float ConsumptionPerSecond;
}
