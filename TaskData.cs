using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerCLI
{
    internal class TaskData
    {
        internal string taskId { get; set; }
        internal string description { get; set; }
        internal DateTime createdAt { get; set; }
        internal DateTime updatedAt { get; set; }
    }
}
