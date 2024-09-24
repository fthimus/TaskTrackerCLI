using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTrackerCLI
{
    internal class TaskTrackerLib
    {
        const string ADD = "ADD";
        const string LIST = "LIST";
        const string UPDATE = "UPDATE";
        const string DELETE = "DELETE";
        const string FILE_PATH = "tasks.json";
        internal static void ProcessArguments(string[] args)
        {
            InitializeJsonFile();
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

        internal static void InitializeJsonFile()
        {
            if (!File.Exists(FILE_PATH))
                File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(new List<TaskData>()));
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

            int taskIdToCreate = AppendTaskToFile(args, FILE_PATH);

            Console.WriteLine($"Task added successfully (ID: {taskIdToCreate})");
        }

        internal static int AppendTaskToFile(string[] args, string path)
        {
            string tasksJsonString = File.ReadAllText(FILE_PATH);
            List<TaskData> tasks = JsonSerializer.Deserialize<List<TaskData>>(tasksJsonString);
            int taskIdToCreate = tasks.Count > 0 ? tasks[tasks.Count - 1].taskId + 1 : 1;
            tasks.Add(new TaskData()
            {
                taskId = taskIdToCreate,
                description = args[1],
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            });

            File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true}));

            return taskIdToCreate;
        }
    }
}
