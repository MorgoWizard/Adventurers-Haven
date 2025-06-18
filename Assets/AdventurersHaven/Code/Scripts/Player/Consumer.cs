using UnityEngine;

public class Consumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IConsumable>(out var consumable))
        {
            consumable.Consume();
        }
    }
}