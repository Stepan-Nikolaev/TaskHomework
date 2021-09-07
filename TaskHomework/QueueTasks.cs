using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskHomework
{
    public class QueueTasks
    {
        private List<Task> _queueTasks = new List<Task>();
        private CancellationToken _treatmentQueueTasks;
        private QueueWorks _queueWorks;

        public QueueTasks(int countParallelWorks, QueueWorks queueWorks, CancellationToken treatmentQueueTasks)
        {
            _queueWorks = queueWorks;
            _treatmentQueueTasks = treatmentQueueTasks;

            for (int i = 0; i < countParallelWorks; i++)
            {
                Work currentWork = _queueWorks.GetCurrentWork(i);
                Task newTask = new Task(currentWork.DisplayMessage);
                _queueTasks.Add(newTask);
                newTask.Start();
            }
        }

        public void StartTreatmentQueueWorks()
        {
            while (true)
            {
                if (_treatmentQueueTasks.IsCancellationRequested)
                {
                    Console.WriteLine("Обработка очреди прервана.");
                    break;
                }

                var completedTask = from currentTask in _queueTasks
                                    where currentTask.IsCompleted == true
                                      select currentTask;

                var poolCompletedTasks = completedTask.ToList();

                if (poolCompletedTasks.Count != 0)
                {
                    if (_queueWorks.QueueWorksNotEmpty())
                    {
                        _queueTasks.Remove(poolCompletedTasks.First());
                        Task lockalTask = Task.Factory.StartNew(_queueWorks.GetCurrentWork(0).DisplayMessage);
                        _queueTasks.Add(lockalTask);
                    }
                }
            }
        }
    }
}
