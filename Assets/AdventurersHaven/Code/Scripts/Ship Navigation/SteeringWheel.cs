using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SteeringWheel : XRBaseInteractable
{
    [SerializeField] private Transform _wheelTransform;

    private float _currentAngle;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _currentAngle = FindWheelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        float totalAngle = FindWheelAngle();
        
        float angleDifference = _currentAngle - totalAngle;
        Debug.Log(angleDifference);

        _wheelTransform.Rotate(transform.right, -angleDifference, Space.World);
        
        _currentAngle = totalAngle;
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;
        
        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
            
            Debug.DrawLine(interactor.transform.position, transform.position);
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        Vector3 local = transform.InverseTransformPoint(position);
        return new Vector2(local.z, local.y).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }

    private float FindRotationSensitivity()
    {
        return 1.0f / interactorsSelecting.Count;
    }
}