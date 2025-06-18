using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatedHandsController : MonoBehaviour
{
    [SerializeField] private InputActionReference _gripInputActionReference;
    [SerializeField] private InputActionReference _triggerInputActionReference;

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    private void Awake()
    {
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateGrip();
    }

    private void AnimateGrip()
    {
        _gripValue = _gripInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }

    private void AnimateTrigger()
    {
        _triggerValue = _triggerInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
