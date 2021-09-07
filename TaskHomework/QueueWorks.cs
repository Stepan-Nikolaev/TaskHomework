using System;
using System.Collections.Generic;
using System.Text;

namespace TaskHomework
{
    public class QueueWorks
    {
        private List<Work> _queueWorks = new List<Work>();

        public QueueWorks(int countWorks)
        {
            for(int i = 0; i < countWorks; i++)
            {
                AddWork(i.ToString());
            }
        }

        public void AddWork(string message = "Добавленная задача")
        {
            _queueWorks.Add(new Work(message));
        }

        public Work GetCurrentWork(int indexWork)
        {
            Work currentWork = _queueWorks[indexWork];
            _queueWorks.Remove(currentWork);

            return currentWork;
        }

        public bool QueueWorksNotEmpty()
        {
            if (_queueWorks.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearQueueWorks()
        {
            _queueWorks.Clear();
        }
    }
}
