using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnedObjects;
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private Transform _spawnPointTransform;
    [SerializeField] private int _objectCount;
    [SerializeField] private float _timeBetweenSpawn;

    private void Spawn()
    {
        if (_spawnedObjects.Count <= _objectCount)
        {
            GameObject spawnedObject = Instantiate(_gameObjectPrefab, _spawnPointTransform.position,
                _spawnPointTransform.rotation);
            _spawnedObjects.Add(spawnedObject);
        }
    }

    private void Awake()
    {
        InvokeRepeating("Spawn", 0, _timeBetweenSpawn);
    }
}
