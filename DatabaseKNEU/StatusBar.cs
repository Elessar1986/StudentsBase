using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseEngineNS;


namespace DatabaseEngineNS
{
    class StatusBar
    {
        Task task;

        public StatusBar()
        {
            task = new Task(DisplayStatusBar);
            task.Start();

        }

        public void DisplayStatusBar()
        {
            while (true)
            {
                Console.SetCursorPosition(1, 39);
                Console.Write($"Status bar : {DateTime.Now.ToString()}");
                Thread.Sleep(100);
            }
        }

    }
}
