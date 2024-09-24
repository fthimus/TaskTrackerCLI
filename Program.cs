using System.ComponentModel;
using System.Text.Json;

namespace TaskTrackerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                TaskTrackerLib.ShowUsage();
                return;
            }

            TaskTrackerLib.ProcessArguments(args);
        }
    }
}