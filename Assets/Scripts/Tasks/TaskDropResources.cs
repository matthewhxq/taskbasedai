using SimpleTaskBasedAI;
using UnityEngine;

public class TaskDropResources : Task
{
    [SerializeField] private Base _base;

    private Backpack _backpack;
    private IMoveController _controller;

    public Backpack Backpack => _backpack ?? (_backpack = GetComponent<Backpack>());
    public IMoveController Controller => _controller ?? (_controller = GetComponent<IMoveController>());

    public override void OnStart()
    {
        Controller.SetDestination(_base.transform.position, OnTargetReached);
    }

    private void OnTargetReached()
    {
        _base.CollectedResources += Backpack.CollectedAmount;
        Backpack.CollectedAmount = 0;

        Finish();
    }

    public override bool IsMeetingConditions()
    {
        return Backpack.CollectedAmount > 0;
    }
}
