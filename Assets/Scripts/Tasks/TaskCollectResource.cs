using SimpleTaskBasedAI;
using UnityEngine;

public class TaskCollectResource : Task
{
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private float _searchRadius;
    [SerializeField] private int _takeAmount;

    private Resource _resource;
    private Backpack _backpack;
    private IMoveController _controller;

    public Backpack Backpack => _backpack ?? (_backpack = GetComponent<Backpack>());
    public IMoveController Controller => _controller ?? (_controller = GetComponent<IMoveController>());

    public override void OnStart()
    {
        var resourceObj = Physics2D.OverlapCircle(transform.position, _searchRadius, _resourceLayer);
        _resource = resourceObj?.GetComponent<Resource>();

        if (_resource != null)
        {
            Controller.SetDestination(_resource.transform.position, OnComplete);
        }
    }

    public override bool IsMeetingConditions()
    {
        return Backpack.CollectedAmount <= 0;
    }

    private void OnComplete()
    {
        Backpack.CollectedAmount += _takeAmount;
        _resource.Amount -= _takeAmount;
        _resource = null;

        Finish();
    }
}
