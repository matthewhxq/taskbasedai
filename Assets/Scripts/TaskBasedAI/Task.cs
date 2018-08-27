using System;
using UnityEngine;

namespace SimpleTaskBasedAI
{
    public abstract class Task : MonoBehaviour, IComparable<Task>
    {
        public int Priority;

        /// <summary>
        /// Called on task start
        /// </summary>
        public abstract void OnStart();

        /// <summary>
        /// Checks whether task can be performed or not
        /// </summary>
        /// <returns></returns>
        public abstract bool IsMeetingConditions();

        private Action _onTaskCompleted;
        private bool _isRunning;

        /// <summary>
        /// Starts new task from begginig
        /// </summary>
        /// <param name="onTaskCompleted"></param>
        public void Run(Action onTaskCompleted)
        {
            if (IsMeetingConditions() == false)
            {
                Debug.LogWarning("Trying to run a task that does not meet it's conditions");
                return;
            }

            _onTaskCompleted = onTaskCompleted;

            OnStart();
        }

        /// <summary>
        /// Call to complete the task
        /// </summary>
        public void Finish()
        {
            _onTaskCompleted?.Invoke();
        }

        public int CompareTo(Task task)
        {
            return Priority.CompareTo(task.Priority);
        }
    }
}
