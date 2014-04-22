using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using QlikView.Connector;

namespace QlikView.ScheduleRunning
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Task parameters {0}.",string.Join(",",args));
            ILog logger = new QVScheduleRunLog();
            logger.Message(string.Format("Task parameters {0}.", string.Join(",", args)));
            try
            {
                if (args.Count() == 1)
                {
                    string task = args[0];
                    HostForm form = new HostForm();
                    form.Logger = logger;
                    form.Show();
                    form.Run(task);
                }
                else
                {
                    logger.Error("Task parameter error.");
                    Console.WriteLine("Task parameter error.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);                

                logger.Error(ex.StackTrace);
            }

        }
    }
}
