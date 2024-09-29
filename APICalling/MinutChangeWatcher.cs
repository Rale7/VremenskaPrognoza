using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VremenskaPrognoza.APICalling
{
    internal class MinutChangeWatcher
    {
        private DateTime lastMinute;
        public readonly Func<Task> taskToRun;
        private Thread thread;
        
        public MinutChangeWatcher(Func<Task> taskToRun) {            
            this.taskToRun = taskToRun ?? throw new ArgumentNullException(nameof(taskToRun));
            lastMinute = DateTime.Now;

            thread = new Thread(run);
            thread.IsBackground = true;
            thread.Start();
        }
        
        private void run()
        {
            while (true)
            {
                Thread.Sleep(1000);
                CheckMinuteChange();
            }
        }

        private void CheckMinuteChange()
        {
            DateTime currentDateTime = DateTime.Now;
            
            if (lastMinute.Minute != currentDateTime.Minute)
            { 
                lastMinute = currentDateTime;
                OnMinuteChange();
            }
        }

        private void OnMinuteChange() {            
            Task.Run(taskToRun);
        }
    }
}
