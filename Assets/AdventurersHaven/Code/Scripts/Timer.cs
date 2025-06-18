using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _minTime, _maxTime;
    [SerializeField] private bool _autoRestart = true;
    [SerializeField] private bool _startOnAwake = true;

    private float _currentTime;
    private bool _isRunning;

    public event Action OnComplete;

    private void Awake()
    {
        if(_startOnAwake) StartTimer();
    }

    public void StartTimer()
    {
        _currentTime = Random.Range(_minTime, _maxTime);
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
        enabled = false;
    }

    private void Update()
    {
        if (!_isRunning) return;

        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0f)
        {
            OnComplete?.Invoke();

            if (_autoRestart)
                _currentTime = Random.Range(_minTime, _maxTime);
            else
                StopTimer();
        }
    }
}