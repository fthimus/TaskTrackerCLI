using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerCLI
{
    internal class TaskTrackerLib
    {
        const string ADD = "ADD";
        const string LIST = "LIST";
        const string UPDATE = "UPDATE";
        const string DELETE = "DELETE";
        internal static void ProcessArguments(string[] args)
        {
            switch (args[0].ToUpper())
            {
                case ADD:
                    AddTask(args);
                    break;
                case LIST:
                    //AddTask();
                    break;
                case UPDATE:
                    //AddTask();
                    break;
                case DELETE:
                    //AddTask();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        internal static void ShowUsage()
        {
            Console.WriteLine("Show usage.");
        }

        internal static void AddTask(string[] args)
        {
            if (args.Length != 2)
            {
                ShowUsage();
                return;
            }
        }
    }
}
