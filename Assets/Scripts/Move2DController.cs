using System;
using SimpleTaskBasedAI;
using UnityEngine;

public class Move2DController : MonoBehaviour, IMoveController
{
    [Range(1, 10)]
    [SerializeField] private float _speed;
    [Range(1, 3)]
    [SerializeField] private float _stoppingDistance;

    private Action _onTargetReached;
    private Vector3? _target;

    public void SetDestination(Vector3 target, Action onTargetReached)
    {
        _target = target;
        _onTargetReached = onTargetReached;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        var direction = (_target.Value - transform.position).normalized;

        transform.position = transform.position + direction * _speed * Time.deltaTime;

        if (IsTargetReached())
        {
            OnTargetReached();
        }
    }

    private void OnTargetReached()
    {
        _target = null;
        _onTargetReached?.Invoke();
        
    }

    private bool IsTargetReached()
    {
        return _target != null && Vector3.Distance(_target.Value, transform.position) <= _stoppingDistance;
    }
}
