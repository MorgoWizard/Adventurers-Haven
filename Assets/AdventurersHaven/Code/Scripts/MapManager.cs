using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Anchors")] 
    [SerializeField] private Transform _neAnchor; 
    [SerializeField] private Transform _swAnchor;
    
    private Vector2 _neCoordinate;
    private Vector2 _swCoordinate;

    [Header("Models")] 
    [SerializeField] private Transform _shipModel;
    [SerializeField] private Transform _targetModel;

    [Header("Settings")]
    [SerializeField] private Vector2 _targetPosition;

    [SerializeField] private ShipNavigationManager _shipNavigationManager;

    private void Start()
    {
        _neCoordinate = _shipNavigationManager.ShipModel.Position;
        _swCoordinate = _targetPosition;
        
        var horizontalDistance = _targetPosition.x - _neCoordinate.x;
        var verticalDistance = _targetPosition.y - _neCoordinate.y;
        
        horizontalDistance *= 0.1f;
        verticalDistance *= 0.1f;
        
        _neCoordinate.x -= horizontalDistance;
        _neCoordinate.y -= verticalDistance;
        
        _swCoordinate.x += horizontalDistance;
        _swCoordinate.y += verticalDistance;
        
        var shipCoordinate = _shipNavigationManager.ShipModel.Position;
        
        _shipModel.position = LocalToWorldPosition(shipCoordinate);
        _shipModel.rotation = GetRotation3D(_shipNavigationManager.ShipModel.Direction);
        _targetModel.position = LocalToWorldPosition(_targetPosition);
    }
    
    public Vector3 LocalToWorldPosition(Vector2 localPos)
    {
        // Нормализуем координаты (приводим к диапазону [0, 1])
        float normX = Mathf.InverseLerp(_swCoordinate.x, _neCoordinate.x, localPos.x);
        float normY = Mathf.InverseLerp(_swCoordinate.y, _neCoordinate.y, localPos.y);
        
        normX = Mathf.Clamp01(normX);
        normY = Mathf.Clamp01(normY);

        // Лерпим между якорями в мировом пространстве
        Vector3 worldPos = new Vector3(
            Mathf.Lerp(_swAnchor.position.x, _neAnchor.position.x, normX),
            Mathf.Lerp(_swAnchor.position.y, _neAnchor.position.y, normY), // если нужна высота
            Mathf.Lerp(_swAnchor.position.z, _neAnchor.position.z, normY)  // если работаем в 3D (XZ)
        );

        return worldPos;
    }
    
    public Quaternion GetRotation3D(Vector2 direction)
    {
        // Угол в радианах (0 = восток, +90° = север, -90° = юг)
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, angle-90, 0);
    }

    private void Update()
    {
        _shipModel.position = LocalToWorldPosition(_shipNavigationManager.ShipModel.Position);
        _shipModel.localRotation = GetRotation3D(_shipNavigationManager.ShipModel.Direction);
    }
}
