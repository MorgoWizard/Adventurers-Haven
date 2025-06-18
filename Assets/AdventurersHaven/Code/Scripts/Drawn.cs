using System;
using UnityEngine;

public class Drawn : MonoBehaviour
{
    [SerializeField] private ShipWobble _shipWobble;
    [SerializeField] private float _drawSpeed;

    private bool _isDead;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CannonBall"))
        {
            _shipWobble.enabled = false;
            _isDead = true;
        }
    }

    private void Update()
    {
        if (_isDead) GoDown();
    }

    private void GoDown()
    {
        transform.Translate(0, -_drawSpeed * Time.deltaTime,0);
    }
}
