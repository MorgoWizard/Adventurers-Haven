using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    private XRIDefaultInputActions _inputActionReference;

    private void Awake()
    {
        _inputActionReference = new XRIDefaultInputActions();
        _inputActionReference.Enable();
        _inputActionReference.XRILeftInteraction.Restart.started += Restart;
    }

    private void Restart(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
