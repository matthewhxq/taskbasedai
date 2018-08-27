using System.Collections.Generic;
using UnityEngine;

namespace SimpleTaskBasedAI
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] private List<Task> _tasks = new List<Task>();

        private Task _currentTask;

        private void Start()
        {
            FindAndRunTask();
        }

        private void FindAndRunTask()
        {
            if (_tasks.Count == 0)
            {
                Debug.LogWarning("No tasks defiend");
                return;
            }

            _tasks.Sort();

            for (int i = 0; i < _tasks.Count; i++)
            {
                var task = _tasks[i];
                if (task.IsMeetingConditions())
                {
                    Debug.LogFormat("Starting new task - {0}", task.ToString());
                    _currentTask = task;
                    _currentTask.Run(OnTaskCompleted);
                    return;
                }
            }
        }

        private void OnTaskCompleted()
        {
            Debug.LogFormat("Task completed - {0}", _currentTask?.ToString());
            FindAndRunTask();
        }
    }
}
