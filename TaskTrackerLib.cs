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

        const string STATUS_TODO = "todo";
        const string STATUS_IN_PROGRESS = "in-progress";
        const string STATUS_DONE = "done";

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
                    ListTask(args);
                    break;
                case UPDATE:
                    UpdateTask(args);
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
            List<TaskData>? tasks = ReadTasksFromFile(path);
            int taskIdToCreate = tasks?.Count > 0 ? tasks[tasks.Count - 1].taskId + 1 : 1;
            tasks?.Add(new TaskData()
            {
                taskId = taskIdToCreate,
                description = args[1],
                status = STATUS_TODO,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            });

            File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(tasks, new JsonSerializerOptions() { WriteIndented = true}));

            return taskIdToCreate;
        }

        internal static List<TaskData>? ReadTasksFromFile(string path)
        {
            string tasksJsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<TaskData>>(tasksJsonString);
        }

        internal static void ListTask(string[] args)
        {
            if (args.Length > 2)
            {
                ShowUsage();
                return;
            }

            string statusFilter = args.Length > 1 ? args[1] : string.Empty;

            if (!string.IsNullOrEmpty(statusFilter) &&
                statusFilter.ToLower() != STATUS_TODO &&
                statusFilter.ToLower() != STATUS_IN_PROGRESS &&
                statusFilter.ToLower() != STATUS_DONE)
            {
                ShowUsage();
                return;
            }

            List<TaskData>? tasks = ReadTasksFromFile(FILE_PATH);

            if (tasks == null || tasks.Count == 0)
            {
                Console.WriteLine("No task exist.");
                return;
            }

            foreach (TaskData task in tasks)
            {
                if (!string.IsNullOrEmpty(statusFilter) && task.status.ToLower() != statusFilter.ToLower())
                    continue;
                Console.WriteLine($"ID: {task.taskId}, Description: {task.description}, Status: {task.status}");
            }
        }

        internal static void UpdateTask(string[] args)
        {
            if (args.Length != 3)
            {
                ShowUsage();
                return;
            }

            int idToUpdate = -1;
            try
            {
                idToUpdate = Convert.ToInt32(args[1]);
            }
            catch (Exception ex)
            {
                ShowUsage();
                return;
            }

            List<TaskData>? tasks = ReadTasksFromFile(FILE_PATH);

            TaskData? taskToUpdate = tasks?.FirstOrDefault(x => x.taskId == idToUpdate);

            if (taskToUpdate == null)
            {
                Console.WriteLine($"Task ID {idToUpdate} not found.");
                return;
            }

            taskToUpdate.description = args[2];

            File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(tasks));
        }
    }
}
