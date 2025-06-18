using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _impulseForce;

    [SerializeField] private GameObject _woodVFX;

    private void Awake()
    {
        _rigidbody.AddForce(_impulseForce*transform.forward, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_woodVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
