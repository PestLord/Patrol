using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _idleTime;

    private int _currentIndex;
    private Transform _newPoint;
    private Transform _currentPoint;
    private bool _moving;
    private float _currentTime;
    private float _travelTime;
    
    // Start is called before the first frame update
    void Start()
    {
        GetPoint();
        _moving = true;
        _currentIndex = 0;
    }

    private void GetPoint()
    {
        _currentPoint = transform;
        _newPoint = _points[_currentIndex].GetComponent<Transform>();
        var distance = Vector3.Distance(_currentPoint.transform.position, _newPoint.transform.position);
        _travelTime = distance / _speed;
        _currentIndex++;
        if (_currentIndex >= _points.Length)
        {
            _currentIndex = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            _currentTime += Time.deltaTime;
            
            
            var step = Time.deltaTime * _speed;
            var newPosition = Vector3.MoveTowards(_currentPoint.position, _newPoint.position, step);
            transform.position = newPosition;
            if (_currentTime >= _travelTime)
            {
                ChangeMode();
            }
        }
        else
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _idleTime)
            {
                GetPoint();
                ChangeMode();
            }
        }
    }

    private void ChangeMode()
    {
        _moving = !_moving;
        _currentTime = 0;
    }
}
