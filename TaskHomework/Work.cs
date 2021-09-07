using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskHomework
{
    public class Work
    {
        private string _message;

        public Work(string message)
        {
            _message = message;
        }
        public void DisplayMessage()
        {
            Console.WriteLine(_message);
            Thread.Sleep(2000);
        }
    }
}
