using System;
using UnityEngine;

namespace SimpleTaskBasedAI
{
    public interface IMoveController
    {
        void SetDestination(Vector3 target, Action onTargetReached);
    }
}
