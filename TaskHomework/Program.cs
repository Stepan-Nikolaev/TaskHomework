using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken treatmentQueueTasks = cancellationTokenSource.Token;
            int countWorks;
            int countParallelWorks;

            Console.WriteLine("В процессе работы програмы нажмите Esc, чтобы очистить очередь задач.");
            Console.WriteLine("Нажмите Backspace, чтобы остановить обработчик задач.");
            Console.WriteLine("Нажмите + чтобы добоавить задачу в очередь");
            Console.WriteLine();
            Console.WriteLine("Введите количество задач в очереди на обработку");

            countWorks = Convert.ToInt32(Console.ReadLine());
            QueueWorks queueWorks = new QueueWorks(countWorks);

            Console.WriteLine("Введите количество параллельно выполняемых задач");
            countParallelWorks = Convert.ToInt32(Console.ReadLine());

            QueueTasks queueTasks = new QueueTasks(countParallelWorks, queueWorks, treatmentQueueTasks);

            UserInputReceiver(queueTasks, queueWorks, cancellationTokenSource);

            Task treatmentQueueTask = Task.Run(queueTasks.StartTreatmentQueueWorks);
            treatmentQueueTask.Wait();
        }

        static void UserInputReceiver(QueueTasks queueTasks, QueueWorks queueWorks, CancellationTokenSource cancellationTokenSource)
        {
            Task userInpet = Task.Run(() =>
            {
                while (true)
                {
                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.Escape:
                            queueWorks.ClearQueueWorks();
                            return;
                        case ConsoleKey.Backspace:
                            cancellationTokenSource.Cancel();
                            break;
                        case ConsoleKey.OemPlus:
                            queueWorks.AddWork();
                            break;
                    }
                }
            });
        }
    }
}
