using UnityEngine;

public class SailControllersRenderer : MonoBehaviour
{
    [SerializeField] private Transform _lAnchorTransform1, _lAnchorTransform2;
    [SerializeField] private Transform _rAnchorTransform1, _rAnchorTransform2;
    [SerializeField] private LineRenderer _lLineRenderer, _rLineRenderer;

    private void Awake()
    {
        _lLineRenderer.positionCount = 2;
        _rLineRenderer.positionCount = 2;
    }

    private void Update()
    {
        _lLineRenderer.SetPosition(0, _lAnchorTransform1.position);
        _lLineRenderer.SetPosition(1, _lAnchorTransform2.position);
        
        _rLineRenderer.SetPosition(0, _rAnchorTransform1.position);
        _rLineRenderer.SetPosition(1, _rAnchorTransform2.position);
    }
}
