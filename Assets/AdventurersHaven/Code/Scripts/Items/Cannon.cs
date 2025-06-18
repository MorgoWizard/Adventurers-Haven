using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Cannon : XRBaseInteractable
{
    [Header("Ограничение движения")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private ParticleSystem _processVFX;
    [SerializeField] private ParticleSystem _shootVFX;

    private IXRInteractor interactor;
    private Vector3 localGrabOffset;
    private float pathLength;
    
    [SerializeField] private Transform _shootTransform;
    
    [SerializeField] private GameObject _cannonBallPrefab;

    private bool _isLoaded;

    public void Shoot()
    {
        if (_isLoaded)
        {
            _processVFX.Play();
            Invoke("SpawnCannonBall", 3f);
            _isLoaded = false;
        }
    }

    private void SpawnCannonBall()
    {
        Instantiate(_cannonBallPrefab, _shootTransform.position, _shootTransform.rotation);
        _shootVFX.Play();
    }

    protected override void Awake()
    {
        base.Awake();
        pathLength = Vector3.Distance(pointA.position, pointB.position);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        interactor = args.interactorObject;
        var grabWorldOffset = interactor.transform.position - transform.position;
        localGrabOffset = Quaternion.Inverse(transform.rotation) * grabWorldOffset;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactor = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("FireStick"))
        {
            Shoot();
        }
    }
    

    private void Update()
    {
        if (interactor == null) return;

        // Пересчитываем позицию руки с учётом offset
        Vector3 targetWorldPos = interactor.transform.position - transform.rotation * localGrabOffset;

        // Проецируем позицию на отрезок
        Vector3 newPosition = ProjectPointOnLine(pointA.position, pointB.position, targetWorldPos);
        transform.position = newPosition;
    }

    private Vector3 ProjectPointOnLine(Vector3 start, Vector3 end, Vector3 point)
    {
        Vector3 dir = (end - start).normalized;
        float dot = Vector3.Dot(point - start, dir);
        float clamped = Mathf.Clamp(dot, 0, pathLength);
        return start + dir * clamped;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reload"))
        {
            _isLoaded = true;
            Destroy(other.gameObject);
        }
    }
}